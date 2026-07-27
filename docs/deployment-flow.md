# DevOpsTool - Deployment Flow

End-to-end workflow for the **Custom DevOps Deployment Tool** — a
configuration-driven **.NET MAUI Blazor Hybrid** app for building, backing up,
deploying, and rolling back on-premises GSS applications in the **SIT**
environment.

Each stage is **gated**: the next step is enabled only after the previous one
succeeds.

![DevOps deployment flow](./deployment-flow.png)

## Stages

1. **Startup** — Load `appsettings.json`, determine the active environment, enable
   the BUILD tab (gate 1).
2. **Build Application** — Display configured apps, validate paths, run
   `msbuild` (stream logs to UI), write artifacts to the output folder.
3. **Backup target environment (gate 2)** — Validate paths, create a timestamped
   backup folder, copy deployment and env config files.
4. **Deploy to SIT (gate 3)** — Copy artifacts to the IIS release folder, apply
   config overrides, run scripts, validate the destination.
5. **Rollback (optional)** — Restore files and configuration from a backup
   snapshot when needed.
6. **Logging** — All build, release, and exception activity is written to a daily
   log file.

## Editing the diagram

The diagram source is [`deployment-flow.mmd`](./deployment-flow.mmd) (Mermaid).
Re-render after edits:

```bash
# PNG
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.png -s 2 -b white
# SVG (scalable)
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.svg -b white
```
