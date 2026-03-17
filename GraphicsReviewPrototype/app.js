(function () {
  const body = document.body;
  const poseButtons = Array.from(document.querySelectorAll("[data-pose]"));
  const lightingButtons = Array.from(document.querySelectorAll("[data-lighting]"));

  const wardrobeToggles = {
    top: document.getElementById("toggle-top"),
    bottom: document.getElementById("toggle-bottom"),
    socks: document.getElementById("toggle-socks"),
    shoes: document.getElementById("toggle-shoes")
  };

  poseButtons.forEach((button) => {
    button.addEventListener("click", () => {
      const pose = button.dataset.pose;
      body.classList.remove("pose-standing", "pose-wall-sit", "pose-kneeling", "pose-failure");
      body.classList.add(`pose-${pose}`);

      poseButtons.forEach((entry) => entry.classList.toggle("active", entry === button));
    });
  });

  lightingButtons.forEach((button) => {
    button.addEventListener("click", () => {
      const lighting = button.dataset.lighting;
      body.classList.remove("lighting-neutral", "lighting-cold", "lighting-dramatic");
      body.classList.add(`lighting-${lighting}`);

      lightingButtons.forEach((entry) => entry.classList.toggle("active", entry === button));
    });
  });

  Object.entries(wardrobeToggles).forEach(([slot, checkbox]) => {
    checkbox.addEventListener("change", () => {
      body.classList.toggle(`wearing-${slot}`, checkbox.checked);
    });
  });
})();
