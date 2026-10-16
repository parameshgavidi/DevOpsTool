# Chart zoom / resize fix for PGTrading

Apply this patch to the **PGTrading** repository (not DevOpsTool).

```bash
cd /path/to/PGTrading
git apply /path/to/chart-zoom-resize.patch
```

Or copy the files from this folder into matching paths under `PGOne/`.

## What was fixed

1. **Zoom buttons** — flat global JS functions (`pgOneChartZoom`) for reliable MAUI Blazor WebView interop
2. **Resize** — `ResizeObserver` redraws the chart when the container size changes
3. **Stale canvas** — re-resolve canvas from DOM on each render after Blazor re-renders
4. **Giant candles** — default 80 visible bars, cap candle body width, lower min zoom bar count
