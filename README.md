# Crimson Tactics
Crimson Tactics is a 3D Tactics Game.
The gameplay is in the battlefield in isometric maps.

## Grid Generation
The isometric map is created using the Unity 3D cube game object. 
<p align="center">
  <img src="Screenshots/SingleGrid.png" width="100"/>
</p>
Every grid tile is a game object with a set position in the world view.
<p align="center">
  <img src="Screenshots/GridView.png" width="400"/>
</p>
Every grid is shown with a UI when the mouse is hovered over the specific grid.
<p align="center">
  <img src="Screenshots/GridViewwithUI.png" width="400"/>
</p>

## Obstacles
An obstacle is created using a red sphere.
<p align="center">
  <img src="Screenshots/Obstacle.png" width="100"/>
</p>
A ScriptableObject is created to store the data where the obstacle is placed on the grid.
<p align="center">
  <img src="Screenshots/1stObstacleData.png" width="200"/>
</p>
The Grid View with Obstacles : 
<p align="center">
  <img src="Screenshots/ObstacleonGrid.png" width="400"/>
</p>

Another changed data for obstacles.
<p align="center">
  <img src="Screenshots/2ndObstacledata.png" width="200"/>
</p>
The Grid View with Obstacles : 
<p align="center">
  <img src="Screenshots/ObstacleGrid2nd.png" width="400"/>
</p>

## Player 
A player is created and placed on the generated grid. 
<p align="center">
  <img src="Screenshots/PlayeronGrid.png" width="200"/>
</p>
The player checks for obstacles. 
<p align="center">
  <img src="Screenshots/PlayerwithObstacles.png" width="400"/>
</p>

The player moves on the grid while avoiding the obstacles.
![Gameplay GIF](Videos/playermovement.gif)

## Enemy
An enemy is added on the grid using an enemy spawner.
<p align="center">
  <img src="Screenshots/EnemyAdded.png" width="400"/>
</p>

The enemy traces the player and reaches the nearest adjacent tile.
![Gameplay GIF](Videos/enemymovement.gif)
