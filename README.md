## Lv.99 Game Jam 2023
![Build Passing](https://img.shields.io/badge/build-passing-brightgreen)
![Version](https://img.shields.io/badge/version-1.0.0-blue)
[![Itch.io](https://img.shields.io/badge/download-itch.io-%23e3326d)](https://itaruf.itch.io/a-melody-of-spring)
[![Youtube](https://img.shields.io/badge/demo-youtube-%23db1818)](https://www.youtube.com/watch?v=m6ms_KO0sIM)

## 💮 A Melody of Spring - 2D tower climbing game
<div align="center"> 
 Play as a sakura flower and climb your way up to your tree's branch.
 <br>
 Collect petals to go faster and let you carry by the wind.
 <br>
 Dodge anything trying to break your pace.
</ul>
</div>

<!-- Table of Contents -->
## :notebook_with_decorative_cover: Table of contents
- [About the project](#star-about-the-project)
  * [Screenshots](#camera-screenshots)
  * [Tech stack](#space_invader-tech-stack)
  * [Highlights](#star-highlights)
  * [Features](#dart-features)
  * [Challenges encountered](#zap-challenges-encountered)

<!-- About the Project -->
## :star: About the project

 <!-- Screenshots -->
### :camera: Screenshots

<div align="center"> 
  <img width="400px" src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExOGx2OTZubWljNnhtNXl1bWFqN3kxczVlOGd3dXJ0czllMmRtdWswMiZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/AO9DSAcVWOPkfSG9SL/giphy.gif">
</div>

<!-- TechStack -->
### :space_invader: Tech stack

  - **Programming language**: C#.
  - **Game engine**: Unity.
  - **IDE**: Visual Studio Community 2022.
  - **Version control**: Git.

### :star: Highlights 
- **Incoming**

### :dart: Features
<details id="projectDescription" open>
  <summary id="summaryText">...</summary>
  
<ol style="text-align: justify;">
  <li><h4>Gameplay overview</h4>
      <ul>
        <li>The player must climb and collect petals to go faster while dodging enemies that slow them down on
contact. It is meant to be a chill experience and climb from season to season.</li>
      </ul>
  </li>
    
  <li><h4>Movements</h4>
  <ul>
    <li>
      The player automatically climbs the tree, while still being able to move horizontally to pick petals, wind boosts, or to dodge enemies. The faster they go, the more precise they must be to do so.
    </li>
  </ul>
  </li>
  
  <li><h4>Speed boosts</h4>
  <ul>
    <li>Winds
     <ul>
      <li>
       Winds pop from time to time, and give the player a short but powerful speed boost in climbing when they collide with them.
      </li>
     </ul>
    </li>
   <li>Petals
    <ul>
     <li>
      Petals also pop from time to time, and give the player a permanent speed boost in climbing when they pick them up. The player can stack them, up to a maximum amount, but loose all the stacks when they are hit by an enemy.
     </li>
    </ul>
    </li>
  </ul>
  </li>
  
  <br>
  <div align="center"> 
      <img src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExcjl1MW84b3Izc3RscXYyM3RpaTExOWNxcHI5NzN5eTNmc3JmdjhhYyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/0efhrXZw3zQAfuuLaM/giphy-downsized-large.gif" style="display: block; margin: auto;" width="400" />
      <img src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExd2x3N3M4YWJwZ3VyNjBpYXhoaW92cTNpbXdsN3p2dmJxajUzcW5zcyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/yDgAJx888o5vjXjvNn/giphy-downsized-large.gif" style="display: block; margin: auto;" width="400" />
  </div>
  
  <li><h4>Enemies</h4>
  <ul>
    <li>
      Enemies spawn randomly on the map, and move either horizontally or fall towards the player. They slow the player in their progression when they collide, and are harder to dodge when the player climbs faster.
    </li>
  </ul>
  </li>

  <li><h4>Spawners</h4>
  <ul>
    <li>
      Pickable boosts and enemies are spawned dynamically all throughout the game. Their spawn position is calculated according to the player's current position and the space required to avoid spawns at the same location.
    </li>
  </ul>
  </li>
      
  <li><h4>Infinite climbing</h4>
  <ul>
    <li>
      The player moves from season to season, cycling through winter, spring, summer and autumn. When they reach the top of the picture of a season, the season changes to the next season, and the player climbs again starting at the bottom of the picture.
    </li>
  </ul>
  </li>   

  <br>
  <div align="center"> 
      <img src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExdzZibmZ6a3M4Nm96NHF5ZHZ4OXltYnBzM2o0cW51N2I2NmRvcDZ5ayZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/UB0IBqDn7SXFqKAPXJ/giphy-downsized-large.gif" style="display: block; margin: auto;" width="400" />
  </div>
  
</ol>

</details>

### :zap: Challenges encountered 
- Developing an infinite climbing system and reflecting season changes.
