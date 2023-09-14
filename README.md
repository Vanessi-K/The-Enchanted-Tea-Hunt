# The Enchanted Tea Hunt #

![the-enchanted-tea-hunt_promotion](https://github.com/Vanessi-K/The-Enchanted-Tea-Hunt/assets/47862956/d3ac37c4-7841-4c00-a39f-da61cc053e4b)


by Anna Blasinger, Vanessa Köck, Juliana Matsumura, Salóme Kiss

Embark on a whimsical journey with Felix the Friendly Fox in "The Enchanted Tea Hunt." Help Felix retrieve scattered enchanted tea party items in the forest, courtesy of the grumpy wizard owl, Oscar. Navigate obstacles, manage Felix's backpack space, and discover special berries for speed boosts. Playful encounters with Oscar and charming visuals create a relaxing atmosphere. The user interface includes a to-do list, timer, and inventory bar. As you collect all items, a delightful ending awaits, showcasing a joyful tea party. Immerse yourself in this magical world and enjoy the enchantment of "The Enchanted Tea Hunt."

---

During the _Creative Code Lab_ of the third semester of the study program _Creative Computing_ (UAS St. Pölten) the aim was to create a game with Unity. The _Creative Code Lab_ is a two week project in which a final group project must be created. This repository holds the final state of the project _The Enchanted Tea Hunt_. The goal in the game is to collect all missing teaparty items as quick as possible.

## Who did what?

The responsibilities where divided amongst the teammember, al though there was some fluctuation the general roles where:
- **Anna Blasinger**: 3D Modeling and Audio Design with WWise
- **Vanessa Köck**: Development of the game logic in the Unity Engine
- **Juliana Matsumura**: 3D Modeling, Animation and Video Editing
- **Salóme Kiss**: 3D Modeling and UI Elements


## Key Features and Implementation Detail
- 3D Modeling
  1. For the humanoid character we modeled a **Fox** that is walking on two legs. The fox has four animations: **Idle**, **Walk**, **Jump** and **Celebrate**.
  2. One side character is the neighbour wizard **Owl**, this character is in the scene and can mock the fox. It is looping in an **idle**-animation.
  3. The second "side character" is the **moving teacup** which also is one of the collectibles. This object has an animation where it is **running** in a loop through the scene.
  4. Other items we modeled for the game where:
     - Teapot (Collectibles)
     - Cupcake (Collectibles)
     - Spoon (Collectibles)
     - Plate (Scene prop)
     - Magic Wand (Held by the owl)
     - Wizard Hat (Worn by the owl)
     - Backpack (Worn by the fox)
     - Returning-Area (Scene prop)
- Game Audio
  1. All the audio in the game was implemented with WWise
  2. All items that can be interacted with have a sound and there is ambience sound
    - Collectibles (idle sound, collect sound)
    - Running Collectibles (idle sound, collect sound, steps, shouts)
    - Returning items
    - Berries (idle sound, collect sound, activation sound)
    - Ambience (grass rustling, birds, paper rustling)
    - Switch Scenes (door sound)
    - Diary and Inventory Scroll (on open and close)
    - Music in all scenes
    - Winning Sound-effect
    - Fox Sounds
      - Opening dialog box
      - Steps depending on speed and if outdoor or indoor
      - Jump
      - Running into solid assets (trees, walls...)
    - Owl (on triggering dialog box of character)
    - Feedback Sounds (button clicks, "Deny" sound if character is not able to collect an item due to inventory space)
      3. The audio of elements that are scattered in the scene is spatialised
      4. Acoustic feedback like button clicks, collecting items, opening UI views
- Unity Coding
  1. The game has a **main menu** and a few other scenes at the beginning and end, to tell the story and show the controls
  2. There are two main in-game scenes the *Forest* and the *interior of the owl's house*
  3. A **statemanager** object that is keeping track of the players inventory and the collectibles in the scene
  4. The player can be **controlled with the keyboard** (W: moving forward) and the **mouse** (horizontal movement: rotating)
  5. The camera can be tilted and zoom with the mouse and modifier-keys.
  6. Collectibles can be collected by touching them and they have to be returned
  7. When the game is finished it can be restarted
  8. The player has to keep track of an inventory system that only allows for you to collect items if there is still space in the slots
  9. When the player interacts or triggers certain objects a dialogue-popup shows up
  10. Certain objects in the scene trigger hints that tell you the controls
  11. The speed of the player is adjusted by the amount of items (weight of the items); so the more items the player has collected the slower they are
  12. When the player slows down also the walk animation slows down
  13. There are particle effects applied on some objects
  14. The moving teacups have a trail-rendering that shows a part of their path
  15. Post processing was added to change the look of the game
  16. The player can click "Q" to show a list of all the items that must be collected
  17. If the player collides with the stack of paper in the owl-house a diary overlay shows
  18. The timer is pausing as long as overlays (the diary entry and the list) are active
  19. There is a berry that acts as a speed up booster
- C# & Theory of CG&A
    1. The teacups are [following a curve](#moving-teacup) in an automatic procedure

## Implementation Logic Explanation:

### Movement and Camera
The player can move forward by pressing "W". When "W" is pressed a boolean is set in the PlayerMovement script.
If it is released the boolean is set again. In the update a forward translation is happening as long as the *_isWalking*-
Boolean is set to *true*.

The player can only walk forward, but can be rotated by horizontal mouse movement to change the walking direction.
We decided against the *WASD*-movement because we also wanted to make it a little bit harder for the player, so
they kinda get "lost" in the forest.

The camera is following the player, this is achieved by at *Update* setting the camera to the position and rotation
of the player and afterward it is moved by a predefined offset. The offset of the camera can be changed by tilting
and zooming. These actions adjust the offset by their given effect. The tilting and zooming are restricted to a certain
value-range.

- The camera can be tilted by pressing the Left-Mouse-Button and moving the mouse vertically.
- The camera can be zoomed by pressing the Right-Mouse-Button and moving the mouse vertically.

### Statemanager
The state of the collectibles is being kept track of by a statemanager. For the teaparty-collectibles and the berries it
keeps track about their state, like if they are *Not Collected*, *In Player Inventory* or *Returned*. These items are 
also in the statemanager object and are just either *Activated* or *Deactivated* in the *GameStats*-object, 
but not destroyed. 

The collectibles are equipped with a *Layer* that is the same as the Level they should be shown in. When switching between
levels an additional function of the statemanager is called, which handles to show the objects. To show only the right object
first all objects of a *Layer* are searched and from those only the ones that have the state *Not Collected* are set to be
active. If a collectible is *Returned* and the level is "Forest" a representation of this object on the table is set to
active.

### Inventory (Backpack)
The player has a limited inventory of 5 slots, so only five items can be carried at a time; any more items you want to
collect you are just walking through. The backpack is also kept persistent over the two scenes, by being applied to the 
object of the statemanager.

The backpack has references to slots in the UI, where the correct representation of the object is shown as soon as an 
item is collected. This is done by matching up the collectible-type with the sprite that has been set to be used with
this type. 

When an object is collected by the player a references to the Collectible-Component of the Collectible-gameobject is saved
in a list. The position of the item in the list is matched up with the position of number of the slots, so that the new
item can be placed at the next empty slot.

The items have a "weight" (all items the same) that has an impact on the speed. 
For each weight-unit the player speed is reduced by 10%.

### Collectible
The player has to collect different kinds of collectibles:
- Moving Teacup
- Teapot
- Cupcake
- Spoon

The player collects the collectibles just by colliding with them. The player can only collect 5 collectibles at once.
When the player is colliding with a collectible it is checked if the "backpack" is full already, if so the player will 
just walk through the collectible and a deny-sound will play.

#### Moving Teacup
The teapcup items are one of our "characters" they are running on a path to make it harder for the player to catch them.
For this movement the basic *FollowCurve*-Script from class was modified. The modifications are:
- Vector3 instead of Vector2
- The moving item is following all curves till the end of the last one is reached, then it is moving backwards along the curve
- The moving item is always looking into the direction it is moving
- A serialized field for adjusting the speed was added

The cups use a LineRenderer behind them to show their path they also occasionally make sounds.

### Speed Boost Berry
Like the collectibles the speed berries are collected by bumping into them. You can only collect one speed-berry at a
time, it is checked if you already have one in your backpack; it also plays a deny-sound. The speed berry is adjusting
your current speed by 150%, so if you are already slowed down by collectibles in your backpack you won't be that fast 
as if you don't have anything in your bag.

The berry is activated by pressing "E". This is done in the *PlayerMovement*, the berry can only be activated after
it was checked that there is a berry in the backpack. When active a particle effect is started on the player to indicate
that the berry is active.

### Returning items
The items have to be returned to the center of the scene in a moving glowing circular return area. The area glows blue
when you are looking at it from the outside. When you are entering the area it's colour changes to pink/red, as soon as
you exit the area goes back to blue. These changes are made on *OnTriggerEnter* and *OnTriggerExit*. The area has it's own
input-system, so when you press "R" the *OnReturn*-function in the *Returning*-script is invoked. Here a function of the
*Backpack* is initialised to clear all collectibles from the backpack-list and slots. If the function was executed 
successfully (so there were items that could be returned) the return-ring turns green.

If all items were returned the game is ended for that the following things are done:
- The *PlayerMovement*-Component on the Player is deactivated
- The celebration-animation is triggered
- The confetti-particle-system is activated
- The timer is stopped
- A wait of three seconds is initialised before switching to the end-story-screen

### Animation
The player itself has four different animations:
- Idle
- Walk
- Jump
- Celebration

The first three animations are switched by using booleans. The celebration animation is played with a trigger at the end
when all items have been returned to the table.

The *Moving Teacup* is constantly looping through a running animation. 

The neighbour (owl) is moving around his magic staff in an idle loop. 

### Scenes
There are two main scenes the "Forest" and the "Interior of the owl's house"; these are the scenes where the player is
moving in. In these scenes the player can also find collectibles and generally interact with the world.

The other scenes can be found before and after the main gameplay as menus and to navigate.
- Start-Screen
  - The player see's this screen when starting the game
- Controls-Screen
  - The player can see the controls that can be used in the game
- Story-Screen
  - A short intro into the story of the game (and why the cups are running 😊)
- End-Story-Screen
  - This story brings the intro full-circle to a happy ending
- End Screen
  - The player sees their time and can restart the game

### Dialogue Popups
When interacting with some objects a dialogue-popup appears that gives further insight into the story. 
The popup is active on different occasions:
- First time going into the return area
  - Gives a hint on how you can return stuff
- Collecting a certain berry
  - Tells you how to use the berries
- Collecting a certain cupcake
  - Gives a hint on how to look at the item list
- Near the owl
  - The owl is gently mocking the player with random sentences from an array
  - This is not showing up all the time when you near the owl
- Entering the second scene
  - When the player enters the second scene it gives you a hint to look for the owl's diary

When a dialogue should show the triggering object needs a *DialoguePopup* there an array of texts can be set, as well as
a probability on how often the popup is shown. A reference to a *DialogueVisibilityHandler* needs to be set. There is also
a checkbox to call the popup as soon as you are colliding with that object, if that is not set you need to call it yourself
with the provided *ShowDialogue*-function. You can also set a reference to another function in a different script, that
has been placed on a gameobject. This function than will be called when the dialogue popup is shown. This was used to
invoke specif sounds when a certain dialogue is shown; like the "Huhu" of the owl and a "Ha" for the fox.

When *ShowDialogue* is called a random number from 0 to a 100 is selected, if the number is below the set showing-percentage
the function is continued, otherwise it returns. Afterward a random element of the array of text-elements is picked.

The *DialogueVisibilityHandler* is applied on the UI element that shows the dialogue box. Here you have to set the 
duration of how long the dialogue is shown as well as references to the element that should be shown/hidden, and a 
reference to the textbook in the element where the text should be placed.

If anywhere in the scene a dialogue is shown, it is closed before a new one is open.

### List of items (Scroll)
When the player presses *Q* a list overlay is displayed. The list shows how many items of each kind you have already collected. 

The component is attached to the statemanager-object, so it persists over all scenes. In the component you have to set 
labels for te CollectibleTypes you want to show, as well as a reference to the panel where clones of the referenced 
elements (with changed data) are placed.

At start the list calls a function in the statemanager that returns the number of total collectibles in the scene for 
eache type that should be displayed. When the scroll/list is than activated by pressing Q, requests to the statemanager
are made to get back the number of items (for each type) that are in another state than "Not Collected". These
values in combination with the label are than used to write the content on the scrolls.
This UI element is also kept in the *GameStats*-object to make it available in all scenes.

### *Owl's Diary Entry*
In the owl's house the player can find the owl's diary (a stack of paper) when hitting the surrounding collider an overlay 
opens that shows the story from the owl's perspective. The popup can be closed by pressing *X*.

### Particles
Particles where mainly used for making it easier for the player to find objects, since otherwise the items where quite
hidden and could barely be noticed. The particles where generated by using the Unity-Particle-Effect. The emitting-shape,
colour, speed, time to live,... where adjusted to be fit.

Particles where used:
- To help indicate the collectives position, to make it easier to find them
- To indicate an active speed-berry effect
- For a confetti-celebration at the end

### Post Processing
Some basic postprocessing was applied to make the scene more vibrant and also have a bloom, so that the glow effect of
the berry can be showcased.

The following effects are used:
- Bloom
- Ambient Occlusion
- Auto Exposure
- Colour Grading 
- Depth Of Field
- Lens Distortions
- Motion Blur
- Vignette

### Restart
At the end the game can be restarted, this is achieved by setting the Singleton-instance of the statemanager to *null*,
afterwards it is switched to the right scene.


