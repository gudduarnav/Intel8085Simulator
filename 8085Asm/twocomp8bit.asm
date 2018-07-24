; find 2's complement of a 8 bit number X and store in next memory location
; to X
; author Arnav Mukhopadhyay
; Feb 27 2011

     ORG C000H
     LXI H, C100H
     MOV A, M
     CMA
     INR A
     INX H    
     MOV M, A
     HLT

