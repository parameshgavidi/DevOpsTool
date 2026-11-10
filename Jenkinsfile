// Declarative pipeline: build an MSI, silently install it on a Windows node,
// then verify the Windows service is present and running.
//
// PLACEHOLDERS — swap the environment values (and the Build MSI command)
// for your product before the first run.

pipeline {
    agent {
        label 'windows'
    }

    environment {
        // --- product-specific placeholders (swap these) ---
        MSI_NAME       = 'YourProduct.msi'                  // output MSI filename
        SERVICE_NAME   = 'YourWindowsService'               // sc query / Get-Service name
        WIX_PROJECT    = 'installer\\YourProduct.wixproj'   // WiX MSBuild project
        WIX_SOURCE     = 'installer\\YourProduct.wxs'       // WiX source (candle/light)
        MSI_OUTPUT_DIR = 'dist'                             // relative to workspace
        // -------------------------------------------------
    }

    options {
        timestamps()
    }

    stages {
        stage('Build MSI') {
            steps {
                echo "Building ${env.MSI_NAME} from ${env.WIX_PROJECT}"
                bat '''
                    if not exist "%MSI_OUTPUT_DIR%" mkdir "%MSI_OUTPUT_DIR%"

                    REM ============================================================
                    REM PLACEHOLDER: swap this command for your real MSI build.
                    REM
                    REM Option A — WiX MSBuild (.wixproj):
                    msbuild "%WIX_PROJECT%" /p:Configuration=Release /p:OutputPath="%MSI_OUTPUT_DIR%\\" /t:Build /v:minimal
                    REM
                    REM Option B — WiX candle + light (.wxs), if you are not using a .wixproj:
                    REM   candle -nologo -out "%MSI_OUTPUT_DIR%\\YourProduct.wixobj" "%WIX_SOURCE%"
                    REM   light  -nologo -out "%MSI_OUTPUT_DIR%\\%MSI_NAME%" "%MSI_OUTPUT_DIR%\\YourProduct.wixobj"
                    REM
                    REM Option C — custom script:
                    REM   call build-msi.cmd
                    REM ============================================================

                    if not exist "%MSI_OUTPUT_DIR%\\%MSI_NAME%" (
                        echo ERROR: MSI not found at "%MSI_OUTPUT_DIR%\\%MSI_NAME%"
                        exit /b 1
                    )
                '''
            }
        }

        stage('Install MSI') {
            steps {
                echo "Silently installing ${env.MSI_OUTPUT_DIR}\\${env.MSI_NAME}"
                bat '''
                    msiexec /i "%MSI_OUTPUT_DIR%\\%MSI_NAME%" /quiet /norestart /l*v "%WORKSPACE%\\msiexec-install.log"
                    set EXITCODE=%ERRORLEVEL%
                    REM 0 = success; 3010 = success, reboot required (ignored because of /norestart)
                    if %EXITCODE% EQU 0 goto :ok
                    if %EXITCODE% EQU 3010 goto :ok
                    echo msiexec failed with exit code %EXITCODE%. See msiexec-install.log
                    exit /b %EXITCODE%
                    :ok
                '''
            }
        }

        stage('Verify Service') {
            steps {
                echo "Verifying Windows service '${env.SERVICE_NAME}'"
                powershell '''
                    $name = $env:SERVICE_NAME
                    sc.exe query $name
                    $svc = Get-Service -Name $name -ErrorAction SilentlyContinue
                    if (-not $svc) {
                        throw "Service '$name' was not created after MSI install."
                    }
                    Write-Host "Service found: $($svc.Name)  Status=$($svc.Status)  StartType=$($svc.StartType)"
                    if ($svc.Status -ne 'Running') {
                        throw "Service '$name' exists but is not Running (Status=$($svc.Status))."
                    }
                '''
            }
        }
    }

    post {
        always {
            echo "Pipeline finished with status: ${currentBuild.currentResult}"
            archiveArtifacts artifacts: 'dist/*.msi, msiexec-*.log', allowEmptyArchive: true
        }
        success {
            echo "MSI built, installed, and service '${env.SERVICE_NAME}' is running."
        }
        failure {
            echo "Pipeline failed. Check msiexec-*.log and the Verify Service stage on the Windows node."
        }
        cleanup {
            // Best-effort uninstall so the node is left clean for the next run.
            bat '''
                if exist "%MSI_OUTPUT_DIR%\\%MSI_NAME%" (
                    msiexec /x "%MSI_OUTPUT_DIR%\\%MSI_NAME%" /quiet /norestart /l*v "%WORKSPACE%\\msiexec-uninstall.log"
                    echo Uninstall attempted, msiexec exit %ERRORLEVEL%
                )
                exit /b 0
            '''
        }
    }
}
