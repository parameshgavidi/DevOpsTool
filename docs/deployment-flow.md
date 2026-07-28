# DevOpsTool - Deployment Flow

Flowchart based on the **BUILD** and **SIT** screens in the Custom DevOps
Deployment Tool (.NET MAUI Blazor Hybrid app).

```mermaid
flowchart TD
    Start([Launch DevOps Tool]) --> BuildTab[BUILD tab]

    subgraph BUILD["BUILD tab"]
        direction TB
        Config["Configure paths<br/>Repo Root and Output Root"]
        Config --> AppList["Display applications<br/>name, type, status, action"]
        AppList --> BuildChoice{Choose action}
        BuildChoice --> BuildOne[Build selected app]
        BuildChoice --> BuildAll[Build All]
        BuildChoice --> DbRollup["Bundle DB Rollup Scripts<br/>(optional)"]
        BuildOne --> Msbuild["Run msbuild<br/>Configuration=Release, DeployOnBuild=true"]
        BuildAll --> Msbuild
        Msbuild --> BuildLog[Stream progress to Output Log]
        BuildLog --> BuildOk{Build succeeded?}
        BuildOk -- Yes --> Output["Artifacts written to<br/>Output Root folder"]
    end

    BuildTab --> Config
    Output --> SitTab[SIT tab]

    subgraph SIT["SIT deployment tab"]
        direction TB
        SitTab --> Backup["Backup all sources<br/>to timestamped backup folder"]
        Backup --> AppType{Application type?}

        AppType --> WebApi["Web / API / MVC / ASMX"]
        AppType --> WinSvc["Windows Service / MSI"]

        WebApi --> StopPool[Stop App Pool]
        StopPool --> CopyFiles["Copy Files<br/>build-output to IIS folder"]
        CopyFiles --> ConfigOverride["Apply config override<br/>e.g. web.config"]
        ConfigOverride --> ReadyWeb[Status = Ready]

        WinSvc --> InstallMsi[Install MSI]
        InstallMsi --> ConfirmMsi[Confirm install]
        ConfirmMsi --> CopyConfig[Copy Config]
        CopyConfig --> ReadySvc[Status = Ready]

        CopyFiles --> DbActions["Database actions<br/>after file copy completes"]
        DbActions --> BackupDb[Backup DB]
        BackupDb --> ApplySchema[Apply Schema Scripts]

        ReadyWeb -. optional .-> Rollback[Rollback]
    end

    BuildOk -- No --> BuildFail([Build failed])
    ReadyWeb --> DeployLog[Write Deployment Log]
    ReadySvc --> DeployLog
    ApplySchema --> DeployLog
    DeployLog --> Done([Complete])

    classDef ok fill:#1b8a5a,stroke:#0f5132,color:#fff;
    classDef fail fill:#c0392b,stroke:#7b241c,color:#fff;
    classDef tab fill:#2d6cdf,stroke:#1b3f8a,color:#fff;

    class Output,ReadyWeb,ReadySvc,Done ok;
    class BuildFail fail;
    class BuildTab,SitTab tab;
```

## BUILD tab

1. **Configure paths** — Set Repo Root and Output Root.
2. **Applications list** — Shows configured apps with name, type, status, and action.
3. **Choose action** — Build a selected app, **Build All**, or optionally **Bundle DB
   Rollup Scripts**.
4. **Run msbuild** — `Configuration=Release`, `DeployOnBuild=true`.
5. **Output Log** — Streams build progress and results.
6. **Output** — Artifacts written to the Output Root folder.

## SIT deployment tab

1. **Backup** — Back up all sources to a timestamped backup folder.
2. **Deploy by application type**
   - **Web / API / MVC / ASMX** — Stop App Pool → Copy Files → Apply config override
     → Status Ready (Rollback available).
   - **Windows Service / MSI** — Install MSI → Confirm install → Copy Config →
     Status Ready.
3. **Database** — After file copy completes: Backup DB → Apply Schema Scripts.
4. **Deployment Log** — Records backup, deploy, and config override activity.

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
