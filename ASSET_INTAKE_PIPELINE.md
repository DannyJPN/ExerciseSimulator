# Asset Intake Pipeline

## Ucel

Tento dokument definuje, jak prijimat, kontrolovat a zavadet fotorealisticke assety do projektu.

Cile:

- udrzet konzistentni kvalitu
- zamezit nahodnemu import chaosu
- umoznit rychlou kontrolu assetu pred zatazenim do `Unity`

## Intake kategorie

Assety delit na:

- `avatar_body`
- `clothing`
- `animation`
- `environment_prop`
- `environment_surface`
- `material_pack`

## Povinne metadata pro intake

Kazdy intake balicek musi mit manifest s:

- `asset_id`
- `category`
- `display_name`
- `source_tool`
- `license`
- `author`
- `scale_unit`
- `target_pipeline`
- `files`
- `technical_notes`

## Doporucene formaty

### 3D mesh

- `FBX`
- `OBJ` pouze vyjimecne

### Textury

- `PNG`
- `TGA`
- `EXR` pro vybrane mapy

### Material metadata

- textovy manifest

### Animace

- `FBX`

## Naming convention

### Meshes

- `CHR_Avatar01_Body.fbx`
- `CLT_Avatar01_Top_A.fbx`
- `ENV_Gym_Bench_A.fbx`

### Textures

- `CHR_Avatar01_Skin_BaseColor.png`
- `CHR_Avatar01_Skin_Normal.png`
- `CHR_Avatar01_Skin_Mask.png`

### Animace

- `ANM_Avatar01_Walk_Forward.fbx`
- `ANM_Avatar01_WallSit_Enter.fbx`
- `ANM_Avatar01_WallSit_Hold.fbx`

## Minimalni technicke pozadavky

### Avatar body

Povinne:

- humanoidni rig
- realisticke proporce
- scale kompatibilni s `Unity meters`
- materialove mapy pro kuzi
- jasny seznam blendshape nebo facial support, pokud existuji

### Clothing

Povinne:

- kompatibilita s cilovym rigem
- materialove mapy
- urceny slot
- informace, zda jde o wearable mesh nebo skinned mesh

### Animation

Povinne:

- root motion info
- fps
- loop flag
- cilovy avatar rig

### Environment

Povinne:

- scale unit
- collision guidance
- material set
- intended placement

## Intake workflow

1. Dodavatel nebo autor pripravi asset package.
2. K assetu vytvori manifest.
3. Manifest se lokalne overi validatorem.
4. Asset se ulozi do intake slozky.
5. Projde manualni art review.
6. Teprve potom jde do `Unity` importu.
7. Po importu se vytvori prefab a asset dostane status `accepted`.

## Intake slozky

```text
AssetIntake/
  incoming/
  accepted/
  rejected/
  manifests/
```

## Review checklist

### Art review

- vypada asset realisticky
- drzi styl projektu
- nema zjevne low-quality povrchy
- nema problematicke proporce

### Technical review

- scale je korektni
- naming je korektni
- textury jsou kompletni
- rig odpovida ocekavani
- manifest je uplny

### Legal review

- licence je jasna
- lze asset pouzit v komercnim nebo internim projektu dle potreby

## Import pravidla do Unity

### Avatar

- import jako humanoid
- overit bone mapping
- overit material sloty
- nevytvaret final prefab bez testu animaci

### Clothing

- overit clipping v neutralni pose
- zkontrolovat kompatibilitu s `base shorts`
- priradit wardrobe slot

### Animation

- pojmenovat klipy konzistentne
- zkontrolovat root transform
- cistit loop a pose offsets

### Environment

- zkontrolovat pivot
- vytvorit collider strategii
- vytvorit prefab

## Blokacni chyby intake

Asset nesmi do projektu, pokud:

- chybi manifest
- chybi licence
- chybi klicove textury
- scale neni znam
- rig neni kompatibilni
- vizualni kvalita je zjevne mimo cil projektu

## Doporucene prvni intake balicky

1. `Avatar body`
2. `Basic top`
3. `Basic bottom`
4. `Shoes`
5. `Socks`
6. `Wall sit animation set`
7. `Gym wall`
8. `Bench`
9. `Mat`

## Nastroje v tomto workspace

Prilozeny jsou:

- [AssetIntake\README.md](C:\Users\Krupa\Desktop\Private\surrendering\AssetIntake\README.md)
- [AssetIntake\asset-manifest.schema.json](C:\Users\Krupa\Desktop\Private\surrendering\AssetIntake\asset-manifest.schema.json)
- [AssetIntake\validate_asset_manifest.js](C:\Users\Krupa\Desktop\Private\surrendering\AssetIntake\validate_asset_manifest.js)
- ukazkove manifesty v `AssetIntake/manifests/`
