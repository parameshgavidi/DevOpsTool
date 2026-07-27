# DevOpsTool - Deployment Flow

Flowchart for the **Custom DevOps Deployment Tool**, a configuration-driven
**.NET MAUI Blazor Hybrid** Windows application that provides a guided,
repeatable and auditable workflow for building and deploying on-premises GSS
applications across the **SIT** environment.

The workflow is **gated**: each operation is enabled only after its required
predecessor completes successfully, preventing partial or unsafe releases.

![DevOps deployment flow](./deployment-flow.png)

## Stages

1. **Startup / Configuration** - Load `appsettings.json` (environment config, app
   definitions, `repoRoot`, `outputRoot`, logging). The active environment is
   determined by configuration; the BUILD tab is then enabled (gate 1).
2. **Build Application (gate 1)** - Validate project file / source dir / output
   subfolder, run `msbuild`, stream stdout+stderr to the UI log, write artifacts
   to the output folder. Status moves Pending -> Building -> Success/Failed; a
   failure surfaces the last relevant error lines.
3. **Backup target environment (gate 2)** - Only enabled after a successful build.
   Creates a timestamped backup root, copies current deployment + env config
   files and records the file count. A failed backup keeps the environment
   **locked** (no partial backups); success enables Deploy and Rollback.
4. **Deploy to SIT (gate 3)** - Only enabled after a successful backup. Copies
   artifacts to the IIS release folder, applies env/config overrides, runs
   pre/post scripts, then validates the destination.
5. **Rollback** - Available whenever a successful backup exists; restores files
   and configuration from a selected backup snapshot.
6. **Logging (cross-cutting)** - All build/release/exception activity is written
   with timestamp + status to a daily log file in the root folder.

## Editing the diagram

The diagram source is [`deployment-flow.mmd`](./deployment-flow.mmd) (Mermaid).
Re-render after edits:

```bash
# PNG
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.png -s 2 -b white
# SVG (scalable)
npx -y @mermaid-js/mermaid-cli -i docs/deployment-flow.mmd -o docs/deployment-flow.svg -b white
```
