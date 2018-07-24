; Add 2 unsigned integers at 0200h, 0202h, and store the result in
; 0204h
; author Arnav Mukhopadhyay
; Dec 31, 2011

     LHLD 0200H
     MOV B, H
     MOV C, L
     LHLD 0202H
     DAD B
     SHLD 0204H
     HLT
