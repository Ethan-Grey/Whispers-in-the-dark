
# Whispers in the Dark

**Genre:** Horror / Mystery / Exploration  
**Developer:** Ethan Grey  
**Project:** Unity-based first-person horror game  
**Protagonist:** Ethan Granger (Private Investigator)  
**Setting:** Haunted Drakemore Mansion  

---

## Overview

*Whispers in the Dark* is a suspenseful horror game where you play as Ethan Granger, a private investigator delving into the eerie and sinister Drakemore family mansion. The mansion is riddled with paranormal phenomena, dark secrets, and complex puzzles. Your goal is to unravel the mysteries within, survive encounters with supernatural forces, and ultimately uncover the truth behind the mansion’s cursed history.

---

## Storyline

You begin outside the mansion’s front gate, with both the front and side doors locked. The back entrance is accessible after moving some boxes. Inside, the house is cold, dark, and ominous.

The Drakemore mansion holds many secrets, including three mystical rotatable statues outside the mansion, which are key to unlocking the final ritual room and triggering the game’s conclusion.

---

## Key Features

- **Exploration:** Traverse multiple mansion areas: foyer, dining room, bathroom, basement, study, and bedroom.
- **Puzzles:** Multi-step puzzles that reveal hidden rooms, levers, keys, and secrets.
- **Paranormal Encounters:** Face eerie supernatural events and enemies (Watcher represented by the Book Head Monster).
- **Statue Unlock Mechanic:** Solve puzzles to rotate three statues outside the mansion; aligning all statues towards the house unlocks the ritual room.
- **Sanity Mechanic:** Manage your mental stability amidst haunting events.
- **Multiple Endings:** Your choices and success in puzzles influence the game's ending.
- **Checkpoint System:** Save progress at checkpoints to avoid replaying large sections.
- **Timer:** Tracks gameplay duration.
- **Life System:** Manage your health during encounters.
- **Sound Effects:** Immersive audio cues enhance atmosphere and tension.
- **Pause Menu & Game Complete Screen:** Control gameplay and view completion status.

---

## Mansion Puzzle & Objective Details

### Statue Unlocking Puzzles

1. **Statue 1 Puzzle:**  
   - Find a padlocked trunk in the dining area.  
   - Unlock the trunk to retrieve the Drakemore journal.  
   - Return the journal to the bookshelf in the study to open a secret room with a lever.  
   - Use this lever to unlock the first statue’s rotation.

2. **Statue 2 Puzzle:**  
   - Use a pressure plate to unlock the bathroom door.  
   - Inside, find a lever handle and a marble.  
   - Use the lever handle in the basement control panel to progress.  
   - The marble is used in a minigame in the bedroom.  
   - Unlocking this statue involves solving these steps.

3. **Statue 3 Puzzle:**  
   - Complete a minigame in the bedroom using the marble to receive a key.  
   - Use the key to open a trunk containing a portrait.  
   - Return the portrait to a statue to enable its rotation.

### House Progression

- Start outside the mansion gate.  
- Move boxes to access the back door.  
- Find lockpicks inside to open the basement control panel.  
- Solve an electrical puzzle in the basement to power the house and unlock inner doors.  
- Explore the dining room, foyer, bathroom, study, and bedroom for clues and puzzle items.  
- Unlock secret rooms by interacting with discovered objects and completing puzzles.  

---

## Controls

- **Movement:** WASD / Arrow Keys  
- **Look Around:** Mouse  
- **Interact:** E  
- **Pause Menu:** Esc  
- **Inventory / Puzzle UI:** Tab (or as implemented)  

---

## Installation and Setup Guide

### Requirements

- **Unity Version:** 2021.3.0f1 or higher, preferably version **6000+** (check your Unity Hub or project settings)  
- **Render Pipeline:** This game **requires Unity Universal Render Pipeline (URP)** to run correctly. Ensure URP is set up in your project.  
- **Git Large File Storage (Git LFS):** Because the project contains large assets, Git LFS is used. You must install Git LFS before cloning.

### Step-by-Step Installation

1. **Install Git LFS**

   - If you don’t already have Git LFS installed, download and install it from:  
     https://git-lfs.github.com/  
   - After installation, run the following in your terminal or command prompt:  
     ```bash
     git lfs install
     ```

2. **Clone the repository**

   - Use Git to clone the project:  
     ```bash
     git clone <your-repo-url>
     ```  
   - Git LFS will automatically download the large asset files during cloning.

3. **Open the project in Unity**

   - Launch Unity Hub.  
   - Click **Add** and select the cloned project folder.  
   - Make sure the Unity version matches or exceeds 6000+ (you may need to install the correct version through Unity Hub).  

4. **Configure URP**

   - The project is configured to use URP.  
   - If you encounter graphics issues, open **Edit > Project Settings > Graphics** and confirm the Universal Render Pipeline asset is assigned.  
   - Also, check that the **Pipeline Asset** is assigned in **Edit > Project Settings > Graphics**.

5. **Build and Run**

   - In Unity, go to **File > Build Settings**.  
   - Select your target platform (Windows recommended).  
   - Click **Build and Run** to play the game.

---

## Credits

- Developer: Ethan Grey  
- Model Asset (Watcher): Book Head Monster from Unity Asset Store  
- UFPS Asset for player movement and combat system  
- ElevenLabs AI for voice lines  

---

## Future Development Plans

- Implement the final third statue puzzle steps.  
- Add additional paranormal encounters and sound effects.  
- Polish animations and UI elements.  
- Introduce more branching endings based on player choices.

---

## Contact

For questions, feedback, or collaboration:  
**Ethan Grey**  
Email: gethan101@gmail.com  
