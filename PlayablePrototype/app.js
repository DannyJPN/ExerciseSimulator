(function () {
  const profiles = {
    disciplined: {
      label: "Disciplined",
      reliefThreshold: 58,
      failureThreshold: 92,
      reliefText: "Requesting relief. I can continue if you insist.",
      successText: "Wall sit completed. The hold was acceptable."
    },
    sensitive: {
      label: "Sensitive",
      reliefThreshold: 44,
      failureThreshold: 78,
      reliefText: "Please shorten this hold. My legs are already hurting.",
      successText: "Wall sit completed. That was uncomfortable."
    },
    selfCritical: {
      label: "Self-Critical",
      reliefThreshold: 54,
      failureThreshold: 88,
      reliefText: "My form is starting to slip. Please allow a shorter hold.",
      successText: "Wall sit completed, but the form was only just controlled."
    },
    ambitious: {
      label: "Ambitious",
      reliefThreshold: 64,
      failureThreshold: 96,
      reliefText: "I can keep going, but I am close to losing the hold.",
      successText: "Wall sit completed. You can make it harder next time."
    }
  };

  const initialState = () => ({
    profileKey: "disciplined",
    posture: "Idle",
    activeCommand: "None",
    activeExercise: "None",
    interaction: "Free",
    holdElapsed: 0,
    holdTarget: 0,
    holdCounted: true,
    exerciseRunning: false,
    currentPhase: "idle",
    request: null,
    requestRejected: false,
    dialogue: "Trainer panel ready.",
    history: [],
    pendingTimeouts: [],
    legsFatigue: 0,
    legsPain: 0,
    cardio: 0,
    wardrobe: {
      top: true,
      bottom: true,
      shoes: true,
      socks: true
    }
  });

  let state = initialState();
  let intervalId = null;

  const els = {
    avatar: document.getElementById("avatar-figure"),
    dialogue: document.getElementById("dialogue-strip"),
    history: document.getElementById("history-list"),
    requestPanel: document.getElementById("request-panel"),
    requestText: document.getElementById("request-text"),
    countBadge: document.getElementById("count-badge"),
    posture: document.getElementById("status-posture"),
    command: document.getElementById("status-command"),
    exercise: document.getElementById("status-exercise"),
    profile: document.getElementById("status-profile"),
    interaction: document.getElementById("status-interaction"),
    hold: document.getElementById("status-hold"),
    legsFatigueValue: document.getElementById("legs-fatigue-value"),
    legsPainValue: document.getElementById("legs-pain-value"),
    cardioValue: document.getElementById("cardio-value"),
    legsFatigueBar: document.getElementById("legs-fatigue-bar"),
    legsPainBar: document.getElementById("legs-pain-bar"),
    cardioBar: document.getElementById("cardio-bar"),
    wardrobeTop: document.getElementById("wardrobe-top"),
    wardrobeBottom: document.getElementById("wardrobe-bottom"),
    wardrobeShoes: document.getElementById("wardrobe-shoes"),
    wardrobeSocks: document.getElementById("wardrobe-socks"),
    profileSelect: document.getElementById("profile-select"),
    resetButton: document.getElementById("reset-button"),
    acceptRequest: document.getElementById("accept-request"),
    rejectRequest: document.getElementById("reject-request")
  };

  document.querySelectorAll("[data-command]").forEach((button) => {
    button.addEventListener("click", () => runCommand(button.dataset.command));
  });

  document.querySelectorAll("[data-sanction]").forEach((button) => {
    button.addEventListener("click", () => applySanction(button.dataset.sanction));
  });

  els.profileSelect.addEventListener("change", (event) => {
    state.profileKey = event.target.value;
    log(`Profile changed to ${profiles[state.profileKey].label}.`);
    render();
  });

  els.resetButton.addEventListener("click", resetState);

  els.acceptRequest.addEventListener("click", () => {
    if (!state.request) {
      return;
    }

    const requestKind = state.request.kind;
    log(`Trainer accepted request: ${requestKind}.`);

    if (requestKind === "relief") {
      state.holdTarget = Math.max(state.holdElapsed + 3, Math.floor(state.holdTarget * 0.75));
      say("Request accepted. I will finish the shortened hold.");
    } else if (requestKind === "clothing") {
      removeNextClothingPiece("Requested clothing adjustment accepted.");
    } else if (requestKind === "intensity") {
      state.holdTarget += 4;
      say("Understood. The next hold will be harder.");
    }

    state.request = null;
    render();
  });

  els.rejectRequest.addEventListener("click", () => {
    if (!state.request) {
      return;
    }

    const requestKind = state.request.kind;
    log(`Trainer rejected request: ${requestKind}.`);
    state.request = null;

    if (requestKind === "relief") {
      state.requestRejected = true;
      say("Understood. I will continue.");
    } else {
      say("Understood.");
    }

    render();
  });

  function resetState() {
    stopInterval();
    clearScheduled();
    const profileKey = state.profileKey;
    state = initialState();
    state.profileKey = profileKey;
    els.profileSelect.value = profileKey;
    log("Session reset.");
    render();
  }

  function runCommand(command) {
    if (state.exerciseRunning) {
      interruptExercise(command);
      return;
    }

    switch (command) {
      case "wallSit":
        startWallSit();
        break;
      case "stand":
        setSimplePosture("Standing", "Stand");
        break;
      case "sit":
        setSimplePosture("Sitting", "Sit");
        break;
      case "kneel":
        setSimplePosture("Kneeling", "Kneel");
        break;
      case "removeShoes":
        startWardrobeCommand("Remove Shoes", "shoes");
        break;
      case "removeSocks":
        startWardrobeCommand("Remove Socks", "socks");
        break;
      default:
        break;
    }
  }

  function setSimplePosture(posture, label) {
    state.activeCommand = label;
    state.activeExercise = "None";
    state.interaction = "Free";
    state.posture = posture;
    state.currentPhase = "idle";
    say(`${label} command accepted.`);
    log(`${label} command started.`);
    render();
  }

  function startWardrobeCommand(label, slot) {
    state.activeCommand = label;
    state.activeExercise = "None";
    state.posture = "Wardrobe Change";
    state.currentPhase = "wardrobe";
    state.interaction = "Bench";
    log(`${label} command started.`);

    if (!state.wardrobe[slot]) {
      say(`${slot === "shoes" ? "Shoes" : "Socks"} are already removed.`);
      render();
      return;
    }

    say(`Changing wardrobe. ${label.toLowerCase()}.`);
    schedule(() => {
      state.wardrobe[slot] = false;
      state.posture = "Sitting";
      state.interaction = "Bench";
      state.currentPhase = "idle";
      log(`${label} completed.`);
      say(`${slot === "shoes" ? "Shoes" : "Socks"} removed.`);
      render();
    }, 1200);

    render();
  }

  function startWallSit() {
    clearScheduled();
    stopInterval();

    state.activeCommand = "Wall Sit";
    state.activeExercise = "Wall Sit";
    state.exerciseRunning = true;
    state.currentPhase = "moving";
    state.posture = "Moving";
    state.interaction = "Wall";
    state.holdElapsed = 0;
    state.holdTarget = 18;
    state.holdCounted = true;
    state.request = null;
    state.requestRejected = false;
    log("Wall Sit command started.");
    say("Moving to the wall.");
    render();

    schedule(() => {
      if (!state.exerciseRunning) {
        return;
      }
      state.currentPhase = "aligning";
      state.posture = "Aligning";
      say("Aligning for wall sit.");
      log("Avatar aligned to wall interaction point.");
      render();
    }, 1300);

    schedule(() => {
      if (!state.exerciseRunning) {
        return;
      }
      state.currentPhase = "holding";
      state.posture = "Holding Pose";
      say("Wall sit started.");
      log("Wall sit hold started.");
      startExerciseTick();
      render();
    }, 2300);
  }

  function startExerciseTick() {
    stopInterval();
    intervalId = window.setInterval(() => {
      if (!state.exerciseRunning || state.currentPhase !== "holding") {
        return;
      }

      state.holdElapsed += 1;
      const clothingPenalty = state.wardrobe.shoes ? 4 : 0;
      const rejectionPenalty = state.requestRejected ? 6 : 0;

      state.legsFatigue = clamp(state.legsFatigue + 5 + rejectionPenalty * 0.18);
      state.cardio = clamp(state.cardio + 3);
      state.legsPain = clamp(
        state.legsPain + (state.legsFatigue > 55 ? 4.8 : 2.2) + clothingPenalty * 0.25 + rejectionPenalty * 0.2
      );

      maybeCreateRequest();

      if (hasPhysicallyFailed()) {
        triggerFailure();
        return;
      }

      if (state.holdElapsed >= state.holdTarget) {
        finishWallSit();
        return;
      }

      render();
    }, 700);
  }

  function maybeCreateRequest() {
    if (state.request || !state.exerciseRunning) {
      return;
    }

    const profile = profiles[state.profileKey];
    const discomfortScore = (state.legsFatigue * 0.6) + (state.legsPain * 0.7);

    if (discomfortScore >= profile.reliefThreshold) {
      state.request = {
        kind: "relief",
        text: profile.reliefText
      };
      say(profile.reliefText);
      log("Avatar requested relief.");
      render();
    }
  }

  function hasPhysicallyFailed() {
    const profile = profiles[state.profileKey];
    const failureScore = Math.max(state.legsFatigue, state.legsPain + (state.requestRejected ? 8 : 0));
    return failureScore >= profile.failureThreshold;
  }

  function triggerFailure() {
    stopInterval();
    state.exerciseRunning = false;
    state.currentPhase = "failure";
    state.posture = "Physical Failure";
    state.holdCounted = false;
    state.request = null;
    say("My thighs hurt. I cannot hold the wall sit any longer.");
    log("Physical failure reached. Wall Sit not counted.");
    render();
  }

  function finishWallSit() {
    stopInterval();
    state.exerciseRunning = false;
    state.currentPhase = "idle";
    state.posture = "Standing";
    state.request = null;
    state.holdCounted = true;
    say(profiles[state.profileKey].successText);
    log("Wall Sit completed and counted.");

    if (state.profileKey === "ambitious" && state.legsFatigue < 52) {
      schedule(() => {
        if (state.exerciseRunning || state.request) {
          return;
        }
        state.request = {
          kind: "intensity",
          text: "This was manageable. Increase the next hold or add an arm position."
        };
        say(state.request.text);
        log("Avatar requested more intensity.");
        render();
      }, 700);
    }

    render();
  }

  function interruptExercise(newCommand) {
    stopInterval();
    clearScheduled();
    state.exerciseRunning = false;
    state.holdCounted = false;
    state.request = null;
    log(`Wall Sit interrupted by ${prettyCommand(newCommand)}.`);
    say(`${prettyCommand(newCommand)} overrides the current exercise. Wall sit will not count.`);

    state.activeExercise = "None";
    state.interaction = "Free";
    state.currentPhase = "idle";

    switch (newCommand) {
      case "stand":
        setSimplePosture("Standing", "Stand");
        break;
      case "sit":
        setSimplePosture("Sitting", "Sit");
        break;
      case "kneel":
        setSimplePosture("Kneeling", "Kneel");
        break;
      case "removeShoes":
        startWardrobeCommand("Remove Shoes", "shoes");
        break;
      case "removeSocks":
        startWardrobeCommand("Remove Socks", "socks");
        break;
      default:
        break;
    }
  }

  function applySanction(type) {
    if (type === "extendHold") {
      if (state.exerciseRunning && state.activeExercise === "Wall Sit") {
        state.holdTarget += 6;
        say("Sanction applied. The hold has been extended.");
        log("Sanction applied: Extend Hold.");
      } else {
        say("Extend Hold requires an active wall sit.");
        log("Blocked sanction: Extend Hold without active wall sit.");
      }
      render();
      return;
    }

    if (type === "lessClothing") {
      if (state.exerciseRunning) {
        say("Less Clothing is only available between exercises in this prototype.");
        log("Blocked sanction: Less Clothing during active exercise.");
        render();
        return;
      }

      if (!removeNextClothingPiece("Sanction applied. Remove one piece of clothing.")) {
        say("Less Clothing is blocked. No removable clothing remains.");
        log("Blocked sanction: Less Clothing.");
      }
      render();
    }
  }

  function removeNextClothingPiece(dialogue) {
    const order = ["shoes", "socks", "top", "bottom"];
    const nextSlot = order.find((slot) => state.wardrobe[slot]);

    if (!nextSlot) {
      return false;
    }

    state.wardrobe[nextSlot] = false;
    state.posture = "Wardrobe Change";
    state.currentPhase = "wardrobe";
    state.activeCommand = "Less Clothing";
    say(dialogue);
    log(`Sanction applied: Less Clothing removed ${titleCase(nextSlot)}.`);

    schedule(() => {
      state.posture = "Standing";
      state.currentPhase = "idle";
      render();
    }, 900);

    return true;
  }

  function prettyCommand(command) {
    const map = {
      stand: "Stand",
      sit: "Sit",
      kneel: "Kneel",
      removeShoes: "Remove Shoes",
      removeSocks: "Remove Socks"
    };
    return map[command] || command;
  }

  function titleCase(value) {
    return value.charAt(0).toUpperCase() + value.slice(1);
  }

  function stopInterval() {
    if (intervalId !== null) {
      window.clearInterval(intervalId);
      intervalId = null;
    }
  }

  function schedule(callback, delay) {
    const id = window.setTimeout(() => {
      state.pendingTimeouts = state.pendingTimeouts.filter((entry) => entry !== id);
      callback();
    }, delay);
    state.pendingTimeouts.push(id);
  }

  function clearScheduled() {
    state.pendingTimeouts.forEach((id) => window.clearTimeout(id));
    state.pendingTimeouts = [];
  }

  function say(message) {
    state.dialogue = message;
  }

  function log(message) {
    const timestamp = new Date().toLocaleTimeString([], {
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit"
    });

    state.history.unshift(`${timestamp}  ${message}`);
    state.history = state.history.slice(0, 20);
  }

  function clamp(value) {
    return Math.max(0, Math.min(100, value));
  }

  function render() {
    const profile = profiles[state.profileKey];
    els.dialogue.textContent = state.dialogue;
    els.posture.textContent = state.posture;
    els.command.textContent = state.activeCommand;
    els.exercise.textContent = state.activeExercise;
    els.profile.textContent = profile.label;
    els.interaction.textContent = state.interaction;
    els.hold.textContent = `${state.holdElapsed} / ${state.holdTarget} s`;
    els.profileSelect.value = state.profileKey;

    els.legsFatigueValue.textContent = `${Math.round(state.legsFatigue)}%`;
    els.legsPainValue.textContent = `${Math.round(state.legsPain)}%`;
    els.cardioValue.textContent = `${Math.round(state.cardio)}%`;
    els.legsFatigueBar.style.width = `${state.legsFatigue}%`;
    els.legsPainBar.style.width = `${state.legsPain}%`;
    els.cardioBar.style.width = `${state.cardio}%`;

    els.wardrobeTop.textContent = state.wardrobe.top ? "Worn" : "Removed";
    els.wardrobeBottom.textContent = state.wardrobe.bottom ? "Worn" : "Removed";
    els.wardrobeShoes.textContent = state.wardrobe.shoes ? "Worn" : "Removed";
    els.wardrobeSocks.textContent = state.wardrobe.socks ? "Worn" : "Removed";

    els.requestPanel.classList.toggle("hidden", !state.request);
    els.requestText.textContent = state.request ? state.request.text : "";

    if (state.exerciseRunning) {
      els.countBadge.textContent = state.holdCounted ? "Exercise Running" : "Not Counted";
    } else if (state.currentPhase === "failure") {
      els.countBadge.textContent = "Physical Failure";
    } else if (state.activeExercise === "Wall Sit" && state.holdCounted) {
      els.countBadge.textContent = "Counted";
    } else if (state.activeExercise === "Wall Sit" && !state.holdCounted) {
      els.countBadge.textContent = "Not Counted";
    } else {
      els.countBadge.textContent = "Waiting";
    }

    els.history.innerHTML = state.history.map((entry) => `<li>${entry}</li>`).join("");

    const avatarClasses = [
      "avatar",
      avatarClassForState(),
      state.wardrobe.top ? "wearing-top" : "",
      state.wardrobe.bottom ? "wearing-bottom" : "",
      state.wardrobe.shoes ? "wearing-shoes" : "",
      state.wardrobe.socks ? "wearing-socks" : "",
      state.currentPhase === "failure" ? "pain-pulse" : ""
    ].filter(Boolean);

    els.avatar.className = avatarClasses.join(" ");
  }

  function avatarClassForState() {
    switch (state.currentPhase) {
      case "moving":
        return "posture-moving";
      case "aligning":
        return "posture-aligning";
      case "holding":
        return "posture-holding";
      case "wardrobe":
        return "posture-wardrobe";
      case "failure":
        return "posture-failure";
      default:
        if (state.posture === "Standing") {
          return "posture-standing";
        }
        if (state.posture === "Sitting") {
          return "posture-sitting";
        }
        if (state.posture === "Kneeling") {
          return "posture-kneeling";
        }
        return "posture-idle";
    }
  }

  log("Prototype booted.");
  render();
})();
