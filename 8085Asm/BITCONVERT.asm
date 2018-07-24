     LXI H, E100H
     MOV D, M
     MVI C, 08H

LOOP:INX H 
     MOV A, D
      ANI 01H
      MOV M, A
     
     MOV A, D
     RAR
     MOV D, A

     DCR C
     JNZ LOOP
     HLT

 