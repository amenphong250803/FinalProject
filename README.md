# 🎮 Interactive Gameplay (Unity Project)
A 2D Unity project focused on building scalable gameplay systems using C#, OOP, and Finite State Machine (FSM).

---

## 🎥 Demo
👉 (Add your YouTube link here)

---

## 🧠 My Responsibilities
- Entity-based architecture (Entity, StatSystem, Combat, Health) to reuse logic across player and other objects
- Player Controller + Finite State Machine (Idle, Move, Jump, Attack) for clear and scalable behavior management
- Developed Save/Load system using PlayerPrefs to persist player progression
- UI Gameplay (HP bar, Stats, Menu) using Unity Canvas
- Cinemachine Camera + Parallax background

---

## ❌ Not My Responsibility
- Enemy AI, Boss mechanics
- Audio system

---

## 🏗️ Architecture
- **Entity-based architecture**  
  Reuse core logic (Stat, Combat, Health) across different entities

- **Finite State Machine (FSM)**  
  Manage player behavior cleanly and make it easy to extend

- **Modular design**  
  Separate systems (Player, UI, SaveSystem) for maintainability

---

## ⚙️ Features
- Player movement and combat system
- Camera follow using Cinemachine
- Parallax background system
- Save/Load player progression
- HP bar, stats UI and Menu UI
  
---

## 📁 Project Structure
The project is organized into modular systems to separate gameplay logic and improve maintainability:
- `/Player` → Player controller and behavior logic
- `/StateMachine` → Finite State Machine implementation for managing states
- `/StatSystem` → Core stat system (health, attributes)
- `/SaveSystem` → Save/Load logic using PlayerPrefs
- `/Entity` → Base entity logic shared across game objects
- `/Enemies`, `/Boss` → Enemy-related logic (integrated with player systems)
- `/Item` → Item system and interactions
- `/UI` → Gameplay UI (HP bar, stats, menu)
- `/Parallax` → Background visual system
- `/Audio` → Audio handling (not implemented by me)

---

## 🚀 How to Run
1. Clone this repository
2. Open with Unity 6000.0.38f1 (Unity 6) or Newer
3. Open scene: MainScene
4. Press Play

---

## 📌 Notes
This project focuses on gameplay systems and architecture design rather than full game content.
