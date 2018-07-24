; exchange data from array in 0201h and 0300h. where N is given in 0200h
; author Arnav Mukhopadhyay
; Feb 22, 2012

     LXI H, 0200H
     LXI D, 0300H
     MOV B, M
LOOP: INX H
     MOV C, M
     LDAX D
     MOV M, A
     MOV A, C
     STAX D
     INX D
     DCR B
     JNZ LOOP
     HLT



