# Sanctuary of the Flame 🔥 | Game Development Portfolio

**Sanctuary of the Flame** is a 2.5D RPG developed in Unity, featuring a turn-based combat system, party management, and dungeon exploration. This repository showcases the **C# architecture and systems design** behind the game.

> **Note:** This is a code portfolio. Asset files (art, audio, models, animations) are not included in the repository. Game files are available on [Itch.io](https://joshe1129.itch.io/sanctuary-of-the-flame).

---

## 📚 Project Overview

This project demonstrates:
- **Turn-based combat system** with state management
- **Party management system** with character persistence
- **Enemy AI** with state machine patterns (Idle, Patrol, Chase, Attack)
- **Input handling** using Unity's new InputSystem
- **Manager pattern** for game state, party, and enemies
- **Clean architecture** with separation of concerns

---

## 🎮 Game Features

- **Turn-Based Combat**: Strategic battle system with player and enemy actions
- **Party Recruitment**: Rescue and recruit allies to strengthen your team
- **Dungeon Exploration**: 2.5D world combining 3D environments with 2D pixel art characters
- **Enemy AI**: Intelligent enemy behavior with detection and pursuit systems
- **Party Persistence**: Save and load party state across scenes
- **HUD Management**: Dynamic UI displaying party stats and battle information

---

## 💻 Code Architecture

### Core Systems

| System | File | Responsibility |
|--------|------|-------------------|
| **Battle System** | `BattleSystem.cs` | Manages turn order, action execution, win/loss conditions |
| **Party Manager** | `PartyManager.cs` | Singleton managing party composition and persistence |
| **Enemy Manager** | `EnemyManager.cs` | Enemy spawning, state tracking, respawn logic |
| **Player Controller** | `PlayerController.cs` | Input handling and overworld movement |
| **Character Manager** | `CharacterManager.cs` | NPC recruitment and party member visuals |
| **Game Manager** | `GameManager.cs` | Global game state (pause, scene loading, quit) |

### Supporting Systems

- **BattleVisuals.cs**: Visual feedback and animations for battles
- **EnemySimpleAI.cs**: State-based AI with NavMesh pathfinding
- **MemberFollowAI.cs**: Party member follow mechanics in overworld
- **OverworldVisuals.cs**: Dynamic HUD updates for party status
- **AnimationManager.cs**: Animation state management
- **EncounterSystem.cs**: Enemy encounter generation with level scaling

---

## 🛠️ Technical Stack

- **Engine**: Unity 2022.3.25f1
- **Language**: C# (.NET 4.7.1)
- **Input System**: New InputSystem (PlayerControls)
- **UI Framework**: TextMeshPro, Unity UI Sliders
- **AI System**: NavMesh pathfinding
- **Architecture Patterns**: 
  - Singleton (PartyManager, EnemyManager)
  - State Machine (BattleSystem, EnemySimpleAI)
  - Manager Pattern (GameManager, PartyManager)

---

## 📂 Code Organization

```
Assets/Scripts/
├── Systems/
│   ├── BattleSystem.cs          # Turn-based combat engine
│   ├── PartyManager.cs          # Party data management
│   └── EnemyManager.cs          # Enemy spawning & persistence
├── Managers/
│   ├── GameManager.cs           # Global game state
│   ├── CharacterManager.cs      # NPC recruitment
│   └── AnimationManager.cs      # Animation utilities
├── AI/
│   ├── EnemySimpleAI.cs         # Enemy behavior (state machine)
│   └── MemberFollowAI.cs        # Party member following
├── UI/
│   ├── BattleVisuals.cs         # Battle visual feedback
│   └── OverworldVisuals.cs      # Party HUD updates
└── Other/
    ├── PlayerController.cs      # Input & movement
    ├── EncounterSystem.cs       # Enemy encounters
    ├── PartyMemberInfo.cs       # Data container
    └── EnemyInfo.cs             # Enemy definitions
```

---

## 🎯 Key Programming Concepts Demonstrated

- **State Management**: Battle states, enemy states, game pause states
- **Data Structures**: Lists, arrays for party and enemy management
- **Event Handling**: InputSystem callbacks, scene loading events
- **Coroutines**: Turn-based routine management and animations
- **Object Pooling**: Enemy respawn system using saved data
- **Inheritance & Polymorphism**: Base entity systems for battles
- **Singleton Pattern**: Persistent manager instances across scenes
- **Scriptable Objects**: PartyMemberInfo, EnemyInfo for data-driven design

---

## 🎮 Play the Game

The complete game with all assets is available on Itch.io:  
👉 [**Play Sanctuary of the Flame on Itch.io**](https://joshe1129.itch.io/sanctuary-of-the-flame)

---

## 📝 License

This project is for portfolio purposes. Game assets and design are original work.
