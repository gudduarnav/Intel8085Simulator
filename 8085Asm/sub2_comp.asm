; Program to subtract 2 numbers in 0200h and 0201h, 
; using 2's complement method, and save the result in 0202h.
; author Arnav Mukhopadhyay
; Dec 20, 2011

                lda 0200h
                mov b, a
                lda 0201h
                cma
                inr a
                add b
                sta 0202h
                hlt

