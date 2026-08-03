# DevOpsTool - Deployment Flow

Flowchart based on the **BUILD**, **SIT**, **CAT**, and **PROD** screens in the Custom DevOps
Deployment Tool (.NET MAUI Blazor Hybrid app).

![DevOps deployment flow — BUILD, SIT, CAT, PROD](./deployment-flow.png)

```mermaid
flowchart TD
    Start([Launch DevOps Tool]) --> BuildTab[BUILD tab]

    subgraph BUILD["BUILD"]
        direction TB
        Config["Configure paths<br/>Code Repo Root,<br/>Output Root, Build Backup folder"]
        Config --> PullLatest["Pull latest changes<br/>from Code repo"]
        PullLatest --> BuildBackup["Backup previous successful<br/>build files<br/>to Build Backup folder"]
        BuildBackup --> AppList["Display applications<br/>name, type, status, action"]
        AppList --> BuildChoice{Choose action}
        BuildChoice --> BuildOne[Build selected app]
        BuildChoice --> BuildAll[Build All]
        BuildChoice --> DbRollupBtn[Bundle DB Scripts]

        BuildOne --> Msbuild["Run msbuild<br/>Configuration=Release, DeployOnBuild=true"]
        BuildAll --> Msbuild

        DbRollupBtn --> DbBranches["Determine rollup range<br/>-f previous release branch<br/>-t current branch<br/>e.g. GSSv9.1S2 to GSSv9.2S2"]
        DbBranches --> DbScripts["Load scripts from repo<br/>or per-environment folder<br/>header, footer, backup, rollup"]
        DbScripts --> DbBuilder["Run DBRollupScriptBuilder.exe<br/>-f ... -t ..."]
        DbBuilder --> DbCopy["Copy scripts to<br/>{outputRoot}\\db-scripts\\"]

        Msbuild --> BuildLog[Stream progress to Output Log<br/>live UI + log text file]
        DbCopy --> BuildLog
        BuildLog --> BuildOk{Build succeeded?}
        BuildOk -- Yes --> Output["Artifacts written to<br/>Output Root folder<br/>(code + db-scripts)"]
    end

    BuildTab --> Config
    Output --> SitTab[SIT tab]
    BuildOk -- No --> BuildFail([Build failed])

    subgraph SIT["SIT"]
        direction TB
        SitTab --> SitValidate["Validate source, destination,<br/>and backup paths"]
        SitValidate --> SitLocked["Only Copy Files enabled<br/>deploy actions locked"]
        SitLocked --> SitBackup["Run backup<br/>timestamped folder, copy deployment<br/>files and environment config"]
        SitBackup --> SitBackupOk{Backup succeeded?}
        SitBackupOk -- Yes --> SitDeployEnabled["Backup complete<br/>deploy and rollback enabled"]
        SitDeployEnabled --> SitCopyFiles["Copy Files<br/>deploy build artifacts"]
        SitCopyFiles --> SitValidateDeploy["Validate deployment<br/>destination exists, files present,<br/>config override complete"]
        SitValidateDeploy --> SitSuccess[Status = Success]
        SitSuccess --> SitDbScripts["Load DB scripts<br/>from repo or env folder path"]
        SitDbScripts --> SitBackupDb[Backup DB]
        SitBackupDb --> SitStopRepl[Stop Replication SQL script]
        SitStopRepl --> SitPublishDb["Publish DB scripts<br/>header, rollup, footer"]
        SitPublishDb --> SitStartRepl[Start Replication SQL script]
        SitStartRepl --> SitDeployLog[Log completion status]
        SitBackupOk -- No --> SitBackupFail([Backup failed])
        SitSuccess -. optional .-> SitRollback[Rollback]
    end

    SitDeployLog --> CatTab[CAT tab]

    subgraph CAT["CAT environment"]
        direction TB
        CatTab --> CatBackupRequired["Backup required<br/>deployment actions locked until complete"]
        CatBackupRequired --> CatRunBackup["Run backup<br/>timestamped backup folder"]
        CatRunBackup --> CatAppType{Application type?}

        CatAppType --> CatWebApi["Web / API / MVC / ASMX"]
        CatAppType --> CatWinSvc["Windows Service / MSI"]

        CatWebApi --> CatStopPool[Stop App Pool]
        CatStopPool --> CatCopyFiles["Copy Files<br/>build output to IIS folder"]
        CatCopyFiles --> CatConfigOverride["Apply config override<br/>e.g. web.config"]
        CatConfigOverride --> CatReadyWeb[Status = Ready]

        CatWinSvc --> CatInstallMsi[Install MSI]
        CatInstallMsi --> CatConfirmMsi[Confirm install]
        CatConfirmMsi --> CatCopyConfig[Copy Config]
        CatCopyConfig --> CatReadySvc[Status = Ready]

        CatCopyFiles --> CatDbScripts["Load DB scripts<br/>from repo or env folder path"]
        CatDbScripts --> CatBackupDb[Backup DB]
        CatBackupDb --> CatStopRepl[Stop Replication SQL script]
        CatStopRepl --> CatPublishDb["Publish DB scripts<br/>header, rollup, footer"]
        CatPublishDb --> CatStartRepl[Start Replication SQL script]
        CatStartRepl --> CatDbReady[DB scripts deployed]

        CatReadyWeb --> CatDeployLog[Write Deployment Log]
        CatReadySvc --> CatDeployLog
        CatDbReady --> CatDeployLog
        CatReadyWeb -. optional .-> CatRollback[Rollback]
    end

    CatDeployLog --> ProdTab[PROD tab]

    subgraph PROD["PROD environment"]
        direction TB
        ProdTab --> ProdServerSelect["Select target server<br/>e.g. PROD-WEB-01"]
        ProdServerSelect --> ProdBackup["Run backup"]
        ProdBackup --> ProdDrain["Load balancer draining<br/>health.gif to health.dat<br/>remove server from pool"]
        ProdDrain --> ProdDrainWait{Active connections = 0?}
        ProdDrainWait -- No --> ProdDrain
        ProdDrainWait -- Yes --> ProdAppType{Application type?}

        ProdAppType --> ProdWebApi["Web / API / MVC / ASMX"]
        ProdAppType --> ProdWinSvc["Windows Service / MSI"]

        ProdWebApi --> ProdStopPool[Stop App Pool]
        ProdStopPool --> ProdCopyFiles["Copy Files<br/>build output to IIS folder"]
        ProdCopyFiles --> ProdConfigOverride["Apply config override"]
        ProdConfigOverride --> ProdReadyWeb[Status = Ready]

        ProdWinSvc --> ProdInstallMsi[Install MSI]
        ProdInstallMsi --> ProdConfirmMsi[Confirm install]
        ProdConfirmMsi --> ProdCopyConfig[Copy Config]
        ProdCopyConfig --> ProdReadySvc[Status = Ready]

        ProdCopyFiles --> ProdDbScripts["Load DB scripts<br/>from repo or env folder path"]
        ProdDbScripts --> ProdBackupDb[Backup DB]
        ProdBackupDb --> ProdStopRepl[Stop Replication SQL script]
        ProdStopRepl --> ProdPublishDb["Publish DB scripts<br/>header, rollup, footer"]
        ProdPublishDb --> ProdStartRepl[Start Replication SQL script]
        ProdStartRepl --> ProdDbReady[DB scripts deployed]

        ProdReadyWeb --> ProdReturnLb["Return to load balancer<br/>restore health endpoint"]
        ProdReadySvc --> ProdReturnLb
        ProdDbReady --> ProdReturnLb
        ProdReturnLb --> ProdDeployLog[Write Deployment Log]
        ProdReadyWeb -. optional .-> ProdRollback[Rollback]
    end

    ProdDeployLog --> Done([Complete])

    classDef ok fill:#1b8a5a,stroke:#0f5132,color:#fff;
    classDef fail fill:#c0392b,stroke:#7b241c,color:#fff;
    classDef tab fill:#2d6cdf,stroke:#1b3f8a,color:#fff;
    classDef env fill:#5c3d99,stroke:#3d2866,color:#fff;

    class Output,SitSuccess,SitStartRepl,CatReadyWeb,CatReadySvc,CatDbReady,CatStartRepl,ProdReadyWeb,ProdReadySvc,ProdDbReady,ProdStartRepl,Done ok;
    class BuildFail,SitBackupFail fail;
    class BuildTab,SitTab,CatTab,ProdTab tab;
    class SIT,CAT,PROD env;
```

## BUILD

1. **Configure paths** — Set Code Repo Root, Output Root, and Build Backup folder.
2. **Pull latest** — Pull latest changes from the Code repo.
3. **Build backup** — Back up the previous successful build files to the Build Backup folder.
4. **Applications list** — Shows configured apps with name, type, status, and action.
5. **Choose action** — Build a selected app, **Build All**, or **Bundle DB Scripts**.
6. **Build code** — Run msbuild (`Configuration=Release`, `DeployOnBuild=true`) for the selected app or all apps.
7. **DB Rollup Scripts** (Bundle DB Scripts button):
   - Determine rollup range: `-f` previous release branch, `-t` current branch (e.g. `GSSv9.1S2` → `GSSv9.2S2`).
   - Load header, footer, backup, and rollup scripts from repository or per-environment folder path.
   - Run `DBRollupScriptBuilder.exe -f ... -t ...` and copy output to `{outputRoot}\db-scripts\`.
8. **Output Log** — Live logs in the UI and saved to a log text file.
9. **Output** — Code artifacts and DB scripts written to the Output Root folder.

## SIT

1. **Validate paths** — Startup validation of source, destination, and backup paths.
2. **Backup required** — Only Copy Files is enabled; deployment actions are locked.
3. **Run backup** — Create timestamped backup folder; copy deployment files and environment config.
4. **Backup complete** — Status Complete; deploy and rollback actions enabled.
5. **Copy Files** — Deploy build artifacts to the target folder.
6. **Validate deployment** — Confirm destination exists, expected files are present, and config override is complete.
7. **Status = Success** — Application deployment marked successful.
8. **DB deploy** (SIT / CAT / PROD) — Load scripts from repo or env folder → **Backup DB** → **Stop Replication** (SQL) → publish DB scripts (header, rollup, footer) → **Start Replication** (SQL).
9. **Log completion** — Record completion status in the deployment log.

## CAT environment

1. **Backup required** — Deployment actions are locked until backup completes.
2. **Run backup** — Back up all sources to a timestamped backup folder.
3. **Deploy code by application type**
   - **Web / API / MVC / ASMX** — Stop App Pool → Copy Files → Apply config override
     → Status Ready (Rollback available). **Deploy All Web/API Apps** available.
   - **Windows Service / MSI** — Install MSI → Confirm install → Copy Config →
     Status Ready.
4. **DB deploy** — Load scripts from repo or env folder → Backup DB → Stop Replication (SQL) → publish DB scripts → Start Replication (SQL).
5. **Deployment Log** — Records backup, code deploy, DB deploy, and config override activity.

## PROD environment

1. **Select target server** — Choose the production server (e.g. `PROD-WEB-01`).
2. **Run backup** — Back up the server before any deploy actions.
3. **Load balancer draining** — Change `health.gif` to `health.dat` to remove the server from the pool; wait until active connections reach 0.
4. **Deploy code by application type** — Same paths as SIT (Copy Files / Install MSI / Copy Config).
5. **DB deploy** — Load scripts from repo or env folder → Backup DB → Stop Replication (SQL) → publish DB scripts → Start Replication (SQL).
6. **Return to load balancer** — Restore the health endpoint and return the server to the pool.
7. **Deployment Log** — Records backup, draining, deploy, DB deploy, and LB return activity.

## Editing the diagram

On GitHub, the diagram above renders as a live Mermaid flowchart. To edit it,
change the `mermaid` block in this file (or [`deployment-flow.mmd`](./deployment-flow.mmd),
which should stay in sync). Re-render static exports after edits:

```bash
# PNG
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.png -s 2 -b white
# SVG (scalable)
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.svg -b white
```
