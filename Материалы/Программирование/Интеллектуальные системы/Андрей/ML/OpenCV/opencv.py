import numpy as np
import cv2

cap = cv2.VideoCapture(0)


while 1:
    ret, img = cap.read()
    
    imgray = cv2.cvtColor(img,cv2.COLOR_BGR2GRAY) 
    
    ret,thresh = cv2.threshold(imgray,200,255,0)
    
    image, contours, hierarchy = cv2.findContours(thresh,cv2.RETR_TREE,cv2.CHAIN_APPROX_SIMPLE)
	
	
	
    #Все контуры
    #cnt = contours
    #cv2.drawContours(img,cnt,-1,(255,0,0),2)

	
	
	
    # а тут последний как я понимаю
    if len(contours) > 0:
        cnt = contours[len(contours)-1]
        x,y,w,h = cv2.boundingRect(cnt)
        cv2.rectangle(img,(x-25,y-25),(x+w+25,y+h+25),(0,0,255),1)
		
		
		
		
    
    cv2.imshow('img', img)
    cv2.imshow('img1', thresh)
    
    k = cv2.waitKey(30) & 0xff
    if k == 27:
        break

cap.release()
cv2.destroyAllWindows()