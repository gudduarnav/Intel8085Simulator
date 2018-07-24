; 8 bit sub via the 2's complement method
; author Arnav Mukhopadhyay
; jan 24 2012
  
    LHLD 0200H
    MOV A, H
    CMA
    INR A
    ADD L
    MOV L, A
    MVI A, 00H
    ADC A
    MOV H, A
    SHLD 0202H
    HLT
