; Find 2's complement of a number in memory location 0200h and store
; in 0201h
; Written by Arnav Mukhopadhyay
;
; Nov 03, 2011
	LDA 0200H
	CMA
	INR A
	STA 0201H
	HLT
