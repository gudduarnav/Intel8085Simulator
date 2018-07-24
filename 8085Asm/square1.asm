; find the square of a number in 0200h
; apr 07, 2012
; author arnav mukhopadhyay
; concept: n^2 = sum of first n odd numbers

     lda 0200h
     mov b, a
     mvi a, 00h
     mvi c, 01h
loop: add c
     inr c
     inr c
     dcr b
     jnz loop
     sta 0201h
     hlt


