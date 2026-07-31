# DevOpsTool - Deployment Flow

Flowchart based on the **BUILD** and **SIT** screens in the Custom DevOps
Deployment Tool (.NET MAUI Blazor Hybrid app).

```mermaid
flowchart TD
    Start([Launch DevOps Tool]) --> BuildTab[BUILD tab]

    subgraph BUILD["BUILD tab"]
        direction TB
        Config["Configure paths<br/>Code Repo Root, DB Repo Root,<br/>Output Root, Build Backup folder"]
        Config --> PullLatest["Pull latest changes<br/>from Code repo and DB repo"]
        PullLatest --> BuildBackup["Backup previous successful<br/>build files and DB scripts<br/>to Build Backup folder"]
        BuildBackup --> AppList["Display applications<br/>name, type, status, action"]
        AppList --> BuildChoice{Choose action}
        BuildChoice --> BuildOne[Build selected app]
        BuildChoice --> BuildAll[Build All]
        BuildOne --> BuildCodeDb["Build code and DB scripts<br/>msbuild Release + DB script build"]
        BuildAll --> BuildCodeDb
        BuildCodeDb --> BuildLog[Stream progress to Output Log]
        BuildLog --> BuildOk{Build succeeded?}
        BuildOk -- Yes --> Output["Artifacts written to<br/>Output Root folder<br/>(code + DB scripts)"]
    end

    BuildTab --> Config
    Output --> SitTab[SIT tab]

    subgraph SIT["SIT deployment tab"]
        direction TB
        SitTab --> SitBackup["Backup all sources<br/>to timestamped backup folder"]
        SitBackup --> AppType{Application type?}
        SitBackup --> DeployDbScripts["Deploy DB scripts<br/>backup target DB, apply schema scripts"]

        AppType --> WebApi["Web / API / MVC / ASMX"]
        AppType --> WinSvc["Windows Service / MSI"]

        WebApi --> StopPool[Stop App Pool]
        StopPool --> CopyFiles["Copy Files<br/>build output to IIS folder"]
        CopyFiles --> ConfigOverride["Apply config override<br/>e.g. web.config"]
        ConfigOverride --> ReadyWeb[Status = Ready]

        WinSvc --> InstallMsi[Install MSI]
        InstallMsi --> ConfirmMsi[Confirm install]
        ConfirmMsi --> CopyConfig[Copy Config]
        CopyConfig --> ReadySvc[Status = Ready]

        DeployDbScripts --> DbReady[DB scripts deployed]
    end

    BuildOk -- No --> BuildFail([Build failed])
    ReadyWeb --> DeployLog[Write Deployment Log]
    ReadySvc --> DeployLog
    DbReady --> DeployLog
    DeployLog --> Done([Complete])

    ReadyWeb -. optional .-> Rollback[Rollback]

    classDef ok fill:#1b8a5a,stroke:#0f5132,color:#fff;
    classDef fail fill:#c0392b,stroke:#7b241c,color:#fff;
    classDef tab fill:#2d6cdf,stroke:#1b3f8a,color:#fff;

    class Output,ReadyWeb,ReadySvc,DbReady,Done ok;
    class BuildFail fail;
    class BuildTab,SitTab tab;
```

## BUILD tab

1. **Configure paths** — Set Code Repo Root, DB Repo Root, Output Root, and Build Backup folder.
2. **Pull latest** — Pull latest changes from the Code repo and DB repo.
3. **Build backup** — Back up the previous successful build files and DB scripts to the Build Backup folder.
4. **Applications list** — Shows configured apps with name, type, status, and action.
5. **Choose action** — Build a selected app or **Build All**.
6. **Build code and DB scripts** — Run msbuild (`Configuration=Release`, `DeployOnBuild=true`) and build DB scripts.
7. **Output Log** — Streams build progress and results.
8. **Output** — Code and DB script artifacts written to the Output Root folder.

## SIT deployment tab

1. **Backup** — Back up all sources to a timestamped backup folder.
2. **Deploy code by application type**
   - **Web / API / MVC / ASMX** — Stop App Pool → Copy Files → Apply config override
     → Status Ready (Rollback available).
   - **Windows Service / MSI** — Install MSI → Confirm install → Copy Config →
     Status Ready.
3. **Deploy DB scripts** — Back up the target database and apply schema scripts from the build output.
4. **Deployment Log** — Records backup, code deploy, DB script deploy, and config override activity.

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
