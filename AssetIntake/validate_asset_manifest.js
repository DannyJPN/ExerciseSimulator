const fs = require("fs");
const path = require("path");

const allowedCategories = new Set([
  "avatar_body",
  "clothing",
  "animation",
  "environment_prop",
  "environment_surface",
  "material_pack"
]);

const allowedScaleUnits = new Set(["meter", "centimeter", "millimeter"]);
const allowedPipelines = new Set(["unity_hdrp"]);
const allowedSlots = new Set(["top", "bottom", "shoes", "socks"]);
const allowedRigTypes = new Set(["humanoid", "generic", "none"]);
const allowedKinds = new Set(["mesh", "texture", "animation", "material", "preview", "document"]);

function fail(message) {
  console.error(`ERROR: ${message}`);
  process.exitCode = 1;
}

function ensure(condition, message) {
  if (!condition) {
    fail(message);
  }
}

function isNonEmptyString(value) {
  return typeof value === "string" && value.trim().length > 0;
}

function validateFileEntry(entry, index) {
  ensure(entry && typeof entry === "object", `files[${index}] must be an object`);
  ensure(isNonEmptyString(entry.path), `files[${index}].path must be a non-empty string`);
  ensure(isNonEmptyString(entry.kind), `files[${index}].kind must be a non-empty string`);
  ensure(allowedKinds.has(entry.kind), `files[${index}].kind "${entry.kind}" is not allowed`);
}

function validateManifest(manifest) {
  const required = [
    "asset_id",
    "category",
    "display_name",
    "source_tool",
    "license",
    "author",
    "scale_unit",
    "target_pipeline",
    "files"
  ];

  required.forEach((key) => ensure(key in manifest, `Missing required field "${key}"`));

  ensure(isNonEmptyString(manifest.asset_id), "asset_id must be a non-empty string");
  ensure(isNonEmptyString(manifest.display_name), "display_name must be a non-empty string");
  ensure(isNonEmptyString(manifest.source_tool), "source_tool must be a non-empty string");
  ensure(isNonEmptyString(manifest.license), "license must be a non-empty string");
  ensure(isNonEmptyString(manifest.author), "author must be a non-empty string");
  ensure(allowedCategories.has(manifest.category), `category "${manifest.category}" is not allowed`);
  ensure(allowedScaleUnits.has(manifest.scale_unit), `scale_unit "${manifest.scale_unit}" is not allowed`);
  ensure(allowedPipelines.has(manifest.target_pipeline), `target_pipeline "${manifest.target_pipeline}" is not allowed`);

  if ("slot" in manifest) {
    ensure(allowedSlots.has(manifest.slot), `slot "${manifest.slot}" is not allowed`);
  }

  if ("rig_type" in manifest) {
    ensure(allowedRigTypes.has(manifest.rig_type), `rig_type "${manifest.rig_type}" is not allowed`);
  }

  ensure(Array.isArray(manifest.files), "files must be an array");
  ensure(manifest.files.length > 0, "files must not be empty");
  manifest.files.forEach(validateFileEntry);

  if (manifest.category === "avatar_body") {
    ensure(manifest.rig_type === "humanoid", "avatar_body must use rig_type humanoid");
  }

  if (manifest.category === "clothing") {
    ensure("slot" in manifest, "clothing manifest must define slot");
  }
}

function main() {
  const manifestPath = process.argv[2];
  if (!manifestPath) {
    console.error("Usage: node AssetIntake\\validate_asset_manifest.js <manifest.json>");
    process.exit(1);
  }

  const absolutePath = path.resolve(process.cwd(), manifestPath);
  if (!fs.existsSync(absolutePath)) {
    console.error(`ERROR: Manifest not found: ${absolutePath}`);
    process.exit(1);
  }

  let manifest;
  try {
    manifest = JSON.parse(fs.readFileSync(absolutePath, "utf8"));
  } catch (error) {
    console.error(`ERROR: Could not parse JSON: ${error.message}`);
    process.exit(1);
  }

  validateManifest(manifest);

  if (process.exitCode && process.exitCode !== 0) {
    process.exit(process.exitCode);
  }

  console.log(`Manifest valid: ${manifest.asset_id}`);
}

main();
