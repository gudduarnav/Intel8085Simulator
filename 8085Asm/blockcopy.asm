; block transfer data from 0201h to 0300h. where N is given in 0200h
; author Arnav Mukhopadhyay
; Feb 22, 2012

     LXI H, 0200H
     LXI B, 0300H
     MOV D, M
LOOP: INX H
     MOV A, M
     STAX B
     INX B
     DCR D
     JNZ LOOP
     HLT


