# Snow My Gosh
Snow My Gosh is an over-the-shoulder, voxel art, infinite driving game. Snow My Gosh is inspired by old school car games like [Pole Position](https://en.wikipedia.org/wiki/Pole_Position) and [Night Driver](https://en.wikipedia.org/wiki/Night_Driver_(video_game)) as well as minimalist winter games like Vlambeer's [Yeti Hunter](https://www.youtube.com/watch?v=3WHGufXjaRA) and Bob Whitehead's [Skiing](https://en.wikipedia.org/wiki/Skiing_(Atari_2600)). I also manage to bring up Yeti and/or Bigfoot in conversation about once a week so I figured it was time to stick a Yeti-like creature in a game. This was also a great chance to build a 3D game that uses a camera perspective that I haven't worked with. After using an isometric perspective in [Smash Captains](https://github.com/mklewandowski/smash-captains), I wanted to try my hand at making a game that required a different 3D camera orientation.

## Gameplay
Swipe left or right to move. Avoid Abominable Snowbots and Ice Boulders. Collect Hearts to launch power-ups. Collect Coins to unlock new vehicles.

![Snow My Gosh gameplay](https://github.com/mklewandowski/snow-my-gosh/blob/main/Assets/Images/snow-my-gosh-gameplay.gif?raw=true)

## Voxel Art
Snow My Gosh contains 40 playable voxel art vehicles and additional in-game voxel art.

![Snow My Gosh cars](https://github.com/mklewandowski/snow-my-gosh/blob/main/Assets/Images/snow-my-gosh-vehicles.gif?raw=true)

Voxel art is created using Magicavoxel. To add new voxel art to the project, do the following:
- create asset model in Magicavoxel
- export model as a .obj file (multiple files are exported, only the .obj file is needed)
- import the .obj model file into Unity
- change the material on the mesh renderer to `voxelMaterial.mat`
- drop the model into a scene (scale and rotation might need to be adjusted)

## Supported Platforms
Snow My Gosh is designed for use on multiple platforms including:
- iOS
- Android
- Web
- Mac and PC standalone builds

## Running Locally
Use the following steps to run locally:
1. Clone this repo
2. Open repo folder using Unity 2021.3.3f.1
3. Install Text Mesh Pro

## Development Tools
- Created using Unity 2021.3.3f.1
- Code edited using Visual Studio Code
- Voxel art made using [Magicavoxel](https://www.voxelmade.com/magicavoxel/)
- Sounds created using [Bfxr](https://www.bfxr.net/)
- Audio edited using [Audacity](https://www.audacityteam.org/)
- 2D images created and edited using [Paint.NET](https://www.getpaint.net/)

## Credits
Swipe detection based on Jason Weimann's tutorial:
https://www.youtube.com/watch?v=jbFYYbu5bdc
