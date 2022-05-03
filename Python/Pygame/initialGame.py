import pygame
import time
from Phidget22.PhidgetException import *
from Phidget22.Phidget import *
from Phidget22.Devices.VoltageInput import *

screen = pygame.display.set_mode((640, 480))
pygame.display.set_caption("Moving thing")


# game loop

def game():
    image = pygame.image.load('Pygame/images/level1.png')
    image = pygame.transform.scale(image, (640,480))
    bgx = 0

    player = pygame.image.load('Pygame/images/boy.png')
    player = pygame.transform.rotozoom(player, 0, 0.2)
    player_y = 325
    

    ch = VoltageInput()
    
    ch.openWaitForAttachment(5000)
    
    ch.open()
    while True:
        #side scrolling
        screen.blit(image, (bgx-640, 0))
        screen.blit(image, (bgx, 0))
        screen.blit(image, (bgx+640, 0))

        bgx-=1
        if bgx <= -640:
            bgx = 0


        #player logic
        screen.blit(player, (50, player_y)) #fixed x
        

        #testing phidget
        currentVoltage = ch.getVoltage()
        #print("Voltage: " + str(voltage))
        minVoltage = ch.getMinVoltage() # 0
        maxVoltage = ch.getMaxVoltage() # 5
        medianVoltage = minVoltage + maxVoltage / 2
        threshold = 0.5
        voltagePosition = currentVoltage - medianVoltage
        

       

        
        pygame.display.update()
       
        #screen is from 0 at top to 480 at bottom. We want to scale so that we just get position?
        #anything larger than 240 should be bottom half 
        #anything smaller than 240 should be top half
        if voltagePosition <= threshold and voltagePosition >= -threshold:
            player_y = player_y

        else:
            if voltagePosition > 0 and player_y > 0: #up
                
                player_y = player_y - voltagePosition
            if voltagePosition < 0 and player_y < 380:
                player_y = player_y - voltagePosition #down

        

        
       
           
        

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.display.quit()
                exit()
            
               
game()