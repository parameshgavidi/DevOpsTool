# DevOpsTool - Deployment Flow

Simple end-to-end workflow for the **Custom DevOps Deployment Tool** — a
configuration-driven **.NET MAUI Blazor Hybrid** app for building, backing up,
deploying, and rolling back on-premises GSS applications in the **SIT**
environment.

Each step is **gated**: the next step is enabled only after the previous one
succeeds.

![DevOps deployment flow](./deployment-flow.png)

## Stages

1. **Load configuration** — Read `appsettings.json` (environment, apps, paths, logging).
2. **Determine active environment** — Use the configured SIT environment.
3. **Build Application** — Build the app and write artifacts to the output folder.
4. **Backup target environment** — Back up the current SIT deployment before changes.
5. **Deploy to SIT** — Copy artifacts to the release folder and apply config overrides.
6. **Rollback** *(optional)* — Restore from a backup if needed.

## Editing the diagram

The diagram source is [`deployment-flow.mmd`](./deployment-flow.mmd) (Mermaid).
Re-render after edits:

```bash
# PNG
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.png -s 2 -b white
# SVG (scalable)
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.svg -b white
```
