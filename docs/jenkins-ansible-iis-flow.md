# Jenkins CI + Ansible CD — .NET to Windows IIS

End-to-end enterprise flow that replaces the local Jenkins job
which both built GSSWeb **and** copied it into localhost IIS.

Step-by-step notepad: [`docs/fixes/JENKINS-ANSIBLE-IIS-WINDOWS-STEPS.txt`](fixes/JENKINS-ANSIBLE-IIS-WINDOWS-STEPS.txt)

Previous Jenkins-only job (keep as reference, do not use for SIT/CAT/PRD):
[`docs/fixes/JENKINS-GSSWEB-IIS-PIPELINE.txt`](fixes/JENKINS-GSSWEB-IIS-PIPELINE.txt)

![Jenkins CI + Ansible IIS flow](./jenkins-ansible-iis-flow.png)

Plain-text Mermaid source: [`docs/jenkins-ansible-iis-flow.txt`](./jenkins-ansible-iis-flow.txt)

```mermaid
flowchart TD
    Dev([Developer commit]) --> Git[Git repository]
    Git --> CiJob[Jenkins job gssweb-ci]

    subgraph CI["JENKINS CI — windows agent"]
        direction TB
        Checkout[Checkout Git]
        Checkout --> Build["Build GSSWebComponent<br/>MSBuild + copy DLL to Bin"]
        Build --> Tests[Run tests]
        Tests --> Zip["Package artifact<br/>gssweb-iis.zip"]
        Zip --> Scan[SonarQube + Security Gate]
        Scan --> Upload["Upload zip + deploy-manifest<br/>to Artifactory"]
        Upload --> Bundle[Create Release Bundle]
    end

    CiJob --> Checkout
    Bundle --> Req{Environment?}

    Req -->|DEV / SIT| DeployJob[Jenkins job gssweb-deploy]
    Req -->|CAT / PRD| CR[Change Request with deploy-request JSON]
    CR --> Approved{CR approved?}
    Approved -->|No| Wait[Wait]
    Approved -->|Yes| DeployJob

    subgraph CD["ANSIBLE — control node to Windows via WinRM"]
        direction TB
        Ping["Connection test<br/>ucd_{EIR}_ebs"]
        Ping --> Features["Configure Windows Server<br/>IIS + ASP.NET 4.8 features"]
        Features --> Site["App pool v4.0 + IIS site"]
        Site --> Download[Download zip from Artifactory]
        Download --> Backup[Backup IIS site folder]
        Backup --> Copy["Stop app pool, copy files,<br/>start app pool"]
        Copy --> Smoke[Smoke test HTTP]
    end

    DeployJob --> Ping
    Smoke --> Running([Application running on Windows IIS])
```

## Who does what

| Step | Owner | What happens |
| --- | --- | --- |
| 1 | Developer | Commit and push .NET code to Git |
| 2 | Jenkins `gssweb-ci` | Checkout, MSBuild GSSWebComponent, tests, zip `gssweb-iis.zip` |
| 3 | Existing EBS jobs | SonarQube, Security Gate, Artifactory upload, Release Bundle |
| 4 | Deploy request JSON | Names the bundle and the environment |
| 5 | DEV/SIT | Run deploy pipeline now |
| 5b | CAT/PRD | Change Request first, then the same deploy pipeline |
| 6 | Ansible | WinRM as `ucd_{EIR}_ebs`: IIS config, backup, copy files, smoke test |
| 7 | Windows IIS | Application running |

## Editing the diagram

```bash
npx -y @mermaid-js/mermaid-cli -i docs/jenkins-ansible-iis-flow.mmd -o docs/jenkins-ansible-iis-flow.png -s 2 -b white
npx -y @mermaid-js/mermaid-cli -i docs/jenkins-ansible-iis-flow.mmd -o docs/jenkins-ansible-iis-flow.svg -b white
```
