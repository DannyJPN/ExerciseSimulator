# Photoreal Unity HDRP Starter Setup

## Ucel

Tento dokument definuje startovni setup pro fotorealisticky `Unity 6 LTS` projekt.

Cil:

- ziskat realisticky zaklad pro avatar a telocvicnu
- neplest gameplay architekturu s improvizovanym visual setupem
- nastavit rozumne minimum pro prvni `HDRP` vertical slice

## Zakladni rozhodnuti

Pouzit:

- `Unity 6 LTS`
- `HDRP`
- `C#`
- `Cinemachine`
- `Animation Rigging`
- `AI Navigation`

Nepouzivat pro prvni fazi:

- vlastni render pipeline upravy
- proceduralni character shader pipeline
- slozite custom post process efekty

## Doporucena adresarova struktura

```text
Assets/
  Art/
    Characters/
      Avatar01/
        Meshes/
        Materials/
        Textures/
        Rigs/
        Animations/
        Prefabs/
    Clothing/
      Shoes/
      Socks/
      Tops/
      Bottoms/
    Environment/
      Gym/
        Meshes/
        Materials/
        Textures/
        Prefabs/
    Lighting/
    VFX/
  Data/
    Commands/
    Exercises/
    Sanctions/
    Behavior/
    Dialogue/
  Prefabs/
    Avatar/
    Environment/
    UI/
  Scenes/
    Gym_MVP.unity
  Scripts/
    Core/
    Commands/
    Actions/
    Exercises/
    Fatigue/
    Wardrobe/
    Behavior/
    Sanctions/
    UI/
```

## Nutne Unity baliky

- `High Definition RP`
- `Input System`
- `Cinemachine`
- `AI Navigation`
- `Animation Rigging`
- `Visual Effect Graph`
- `Addressables`

Volitelne pozdeji:

- `Timeline`
- `Recorder`

## HDRP Project Settings

### Render Pipeline Asset

Zalozit:

- `HDRP_RenderPipelineAsset_Main`
- `HDRP_DefaultVolumeProfile`

Pro prvni realisticky slice nastavit:

- `Lit Shader Mode = Both`
- `MSAA = Off`
- `SSR = On`
- `SSAO = On`
- `Contact Shadows = On`
- `Volumetric Fog = Light`
- `Decals = On`

### Quality

PC-first profil:

- target 1440p reference
- quality preset `High`
- shadows `High`
- screen space reflections `Medium` az `High`
- ambient occlusion `On`

Poznamka:

- prvni vertical slice ma vypadat dobre v jednom kontrolovanem prostredi, ne byt optimalni pro vsechny stroje

## Kamera

Pouzit `Cinemachine` s temito rigy:

- `FreeLook_Trainer`
- `QuickView_Front`
- `QuickView_Side`
- `QuickView_Legs`
- `QuickView_Torso`

Nastaveni:

- lens ekvivalent `50-70mm` pro neutralni vzhled postavy
- vyhnout se sirokemu zkresleni
- lehky depth of field pouze v prezentacnim pohledu, ne v inspekcnich pohledech

## Osvetleni sceny

Fotorealismus stoji hlavne na svetle a materialech.

Minimalni setup:

- jedno `Directional Light`
- velika area nebo emissive svetla simulujici stropni fitko osvetleni
- reflection probe pro hlavni prostor
- light probe group kolem oblasti pohybu avatara

Scena ma pusobit:

- ciste
- realisticky
- studeneji neutralne
- bez herniho fantasy looku

## Post Processing

Pouzit stridme:

- `Exposure`
- `White Balance`
- `Color Adjustments`
- `Tonemapping`
- `Bloom` pouze minimalne
- `Depth of Field` velmi jemne

Neprehanet:

- vignette
- silny bloom
- barevne stylizace

## Materialovy standard

Pouzivat:

- `HDRP/Lit`
- PBR workflow
- normal map
- mask map
- detail maps jen tam, kde davaji smysl

Materialy pro MVP:

- kuze
- funkcni sportovni textilie
- podlaha telocvicny
- omitka nebo beton zdi
- kov lavicky
- podlozka

## Avatar pipeline

Doporuceny zdroj:

- `Character Creator 4 + iClone`

Alternativa:

- `Blender + realisticky rig + rucni import`

Avatar musi mit:

- humanoidni rig
- oddelitelnou vrstvu obleceni
- realisticke proporce
- kvalitni textury kuze
- normal a roughness workflow

Pro prvni slice doporucit:

- 1 avatar
- 1 telo
- 1 top
- 1 bottom
- 1 par bot
- 1 par ponozek

## Obleceni

Sloty:

- top
- bottom
- shoes
- socks

Technicke pravidlo:

- `base shorts` zustavaji vzdy soucasti tela a nejsou intake item

Kazdy wearable asset musi mit:

- konzistentni scale
- kompatibilitu s rigem avatara
- material variantu
- variantu pro import do prefab workflow

## Animace

Pro prvni fotorealisticky slice jsou nutne:

- idle
- walk forward
- align small steps
- wall sit entry
- wall sit hold
- wall sit failure
- sit
- kneel
- remove shoes
- remove socks

Zdroj:

- kvalitni mocap nebo rucne cistene klipy

Mixamo lze pouzit jen:

- pro blokaci
- ne pro finalni realistickou prezentaci

## Prvni fotorealisticka scena

Obsah:

- zed pro wall sit
- lavicka
- podlozka
- realisticka podlaha
- neutralni stropni svetlo
- bez zbytecnych dekoraci

Proc:

- kazdy dalsi objekt navic zvysuje naroky na lighting, materialy a art consistency

## Minimalni prefab backlog

### Avatar

- `PF_Avatar01_Base`
- `PF_Avatar01_WardrobeRuntime`

### Environment

- `PF_Gym_WallInteraction`
- `PF_Gym_Bench`
- `PF_Gym_Mat`

### Camera

- `PF_CameraRig_Trainer`

## Acceptance criteria pro visual starter

- scena ma konzistentni HDRP lighting
- avatar nevypada stylizovane ani cartoonove
- obleceni se da vrstvovat bez rozbiti siluety
- wall sit oblast je citelna z celniho i bocniho pohledu
- kamera umi rychle prepinat inspekcni pohledy

## Co nepovazovat za hotovy fotorealismus

Nasledujici veci nestaci:

- detailni UI
- pekne CSS mocky
- kvalitni screenshot bez realneho 3D modelu
- jeden high-poly model bez dobrych materialu a svetla

Fotorealisticky vysledek vznikne az kombinaci:

- asset quality
- lighting
- materials
- animation quality
- camera framing
