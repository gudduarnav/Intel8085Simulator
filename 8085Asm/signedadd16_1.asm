; Add 2 signed integers at 0200h, 0202h, store the result in 0204h.
; Store sign in 0206h and magnitude in 0208h
; author Arnav Mukhopadhyay
; Dec 31, 2011

     LHLD 0200H
     MOV B, H
     MOV C, L
     LHLD 0202H
     DAD B
     SHLD 0204H
     XRA A
     ADD H
     JP POS
     XRA A
     ADD L
     CMA
     INR A
     MOV L, A
     MOV A, H
     CMA
     ACI 00H
     MOV H, L
     MVI A, 01H
     JMP STR
POS: MVI A, 00H
STR: STA 0206H
     SHLD 0208H
     HLT