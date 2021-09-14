# Lightsaber Fight
Hyper-Casual Unity Developer Test

Since test document dont provide information about how lightsabers are positioned,rotated initially we assume that orientation and location speed of swing can be changed per lightsaber.

* Lightsaber Features
  * Default rotation can be changed
  * Default location can be changed
  * Swing Speed of each lightsaber can be changed
  * Handle and Laser length can be changed
  * Lightsabers radius can be changed separately 
  * Swing starting rotation to ending rotation can be changed separately

 
  Project Structure
  ----
  Project contains "Data" folder for lightsaber configuration. Each lightsaber
      named as "A" and "B" has respective folders which contains variables change
      various options. 
      
  Changing lightsaber parameters
  ----
   In data folder you will find "lightsaberA" and "lightsaberA" assets. With
      this asset you can change length of the lightsaber`s handle and laser length.
      You can also change radius of the laser.
    
   
  Changing swing parameters
  ----
    In data folder you will find "swingAFromRot" and "swingAToRot" assets for
      both lightsabers A and B. With this asset you can change starting and ending
      coordinates for swing. There is an another asset named "SwingA" and "SwingB"
      which both starting ,ending coordinates as well as duration of swing. You can
      create new paramter file if you wan to change them by right clicking any empty
      space in data folder then select "Create Vector3 Variable". After creating
      rotation parameter you can assing to "SwingA" or "SwingB" assets.

 Changing slider limits
 ----
In data folder you will find "xAMaxAngle" and "xAMinAngle" assets for both
lightsabers A and B. With this asset you can change starting and ending values
for slider.
      
  Changing collision predicton rate
  ----
In Project hierarcy panel you will find "Controllers" game object which
contains component   named "Swing Collision Prediction". On that component
you will see slider named "Angle Resulation" which controls how prediction
is calculated. Default value 1 means it will try every 1 angle change to check
if there is a collision or not. If you increa value to 8 for example it will
check collision at every 8 angle change.

  Changing animation values
  ----
In Project hierarcy panel you will find "Controllers" game object which
contains component   named "Swing Animator". On that component you will see
value  named "Speed Multiplier" which controls how long animation will take.
if you increase that number animation will be slower so that you can inspect
collisions easly. "Animation Reset  Duration" variable is a duration of rewind
animation which returns to starting state. "Wait After Collision Time" controls
the waiting time after collsion happens before rewindding to starting state.
"Wait after swing time " controls waiting time before rewinding if collision
is  not  occured.
  
  Changing lighsaber locations and rotations
  ----
In Project hierarcy panel you will find "Container" game object under that
there will be two new object  "HandA" and "HandB" use the object to freely
moving and rotating. Using any other games object will lead game to unexpected
behaviour.

  
  Initial Class Structure
  ----

![alt text](https://github.com/mefisto/lightsaber/blob/main/UML%20class.png?raw=true)
