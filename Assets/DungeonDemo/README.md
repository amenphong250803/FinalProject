# Edgar-Style Dungeon Demo (No Plugin)
Unity version target: **6000.0.38f1** (should also work with 2022.3+).

This package replicates an Edgar-Unity–style procedural room/dungeon generator **without** using the plugin.
It features:
- Grid-based room generation with door spawners
- Room cap (default 30)
- Boss room as the last valid room
- Player spawns in the first room
- Seeded random (deterministic maps)

## Quick Start
1. Unzip this archive anywhere.
2. Drag the `Assets` folder into your Unity project (merge folders).
3. In Unity, open any scene (or an empty one).
4. From the top menu, run: **Tools → Dungeon Demo → Create Demo**  
   This will:
   - Create minimal prefabs (rooms, closed room, boss room, start room, player)
   - Create a `RoomTemplates` object and wire arrays
   - Create and save a `DemoScene.unity` in `Assets/DungeonDemo/Scenes/`
   - Open the scene ready to Play
5. Press **Play** to generate a dungeon.

## Controls
- Arrow keys / WASD to move the placeholder player

## Tweaks
- Select the **RoomTemplates** object in the hierarchy:
  - **Max Rooms**: set to 30 by default
  - **Use Seed** + **Seed**: enable deterministic layout
  - **Room Size**: default 16 units (grid step)
- To force a specific layout, check **Use Seed** and set a deterministic **Seed** (e.g., 12345).

## Notes
- Prefabs are minimal (empty rooms with door spawn points); rooms are visualized with **Gizmos** in editor & play mode.
- You can replace the generated prefabs with your own art and colliders later; keep the door spawn points and `RoomSpawner` scripts.
