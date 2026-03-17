# Asset Intake

## Ucel

Tato slozka slouzi jako vstupni brana pro fotorealisticke assety.

## Struktura

```text
AssetIntake/
  incoming/
  accepted/
  rejected/
  manifests/
  asset-manifest.schema.json
  validate_asset_manifest.js
```

## Pouziti

1. Vlozit asset package do `incoming/`
2. Vytvorit manifest v `manifests/`
3. Overit manifest:

```powershell
node .\AssetIntake\validate_asset_manifest.js .\AssetIntake\manifests\avatar_cc4_sample.json
```

4. Po schvaleni presunout asset do `accepted/`

## Poznamka

Validacni skript je zamerne jednoduchy a bez externich zavislosti.
