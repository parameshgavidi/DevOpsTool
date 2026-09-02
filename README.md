# DevOpsTool

Custom DevOps Deployment Tool - a configuration-driven **.NET MAUI Blazor Hybrid**
application for building, backing up, deploying and rolling back on-premises GSS
applications across SIT, CAT, and PROD environments.

## Deployment flow

See [`docs/deployment-flow.md`](docs/deployment-flow.md) for the full end-to-end
workflow (BUILD → SIT → CAT → PROD, with backup, DB script deploy, and logging).

![DevOps deployment flow](docs/deployment-flow.png)

Plain-text Mermaid source for copy-paste: [`docs/deployment-flow.txt`](docs/deployment-flow.txt)

## Architecture — split BuildTab into class files

BuildTab `@code` should stay thin (UI only). Move git, build, DB rollup, email,
and logging into `Services/` classes:

- **Start here if you already have `IBuildService` / `BuildService`:**
  [`docs/architecture/EXISTING-BUILDSERVICE-ALIGN.txt`](docs/architecture/EXISTING-BUILDSERVICE-ALIGN.txt)
- Full method map: [`docs/architecture/BUILDTAB-CLASS-STRUCTURE.txt`](docs/architecture/BUILDTAB-CLASS-STRUCTURE.txt)
- Copy-paste skeletons: [`docs/architecture/skeletons/`](docs/architecture/skeletons/)
  (use existing `IBuildService` instead of `ProcessRunner`)
