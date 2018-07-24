; program to perform 16 bit addition of numbers at (0200h, 0202h).
; store the result in 0204h and carry in 0206h
; author Arnav Mukhopadhyay
; Feb 4, 2011

     LHLD 0200H
     XCHG
     LHLD 0202H
     DAD D
     SHLD 0204H
     MVI A, 00H
     ADC A
     STA 0206H
     HLT

     