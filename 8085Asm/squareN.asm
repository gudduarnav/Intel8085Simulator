; find the square of a N numbers from in 0200h
; apr 07, 2012
; author arnav mukhopadhyay
; concept: n^2 = sum of first n odd numbers

     lxi sp, f000h
     lxi h, 0200h
     lxi d, 0300h
     mov b, m
main: inx h
     inx d
     push b
     call square
     pop b
     stax d
     dcr b
     jnz main
     hlt
square: mov b, m
     mvi a, 00h
     mvi c, 01h
loop: add c
     inr c
     inr c
     dcr b
     jnz loop
     ret