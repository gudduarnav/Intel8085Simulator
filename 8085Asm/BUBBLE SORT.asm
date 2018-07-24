; BUBBLE sort array in 0201h, where N is given in 0200h
; author Arnav Mukhopadhyay
; Feb 22, 2012

     LDA 0200H
     MOV B, A  
     MOV D, A
     DCR B
I:   LXI H, 0200H
     MOV C, B
J:   INX H
     MOV A, M
     INX H
     MOV E, M
     DCX H
     CMP E
     JP SKIP_J
     MOV M, E
     INX H
     MOV M, A
     DCX H
SKIP_J: DCR C
     JNZ J
     DCR D
     JNZ I
     HLT
               