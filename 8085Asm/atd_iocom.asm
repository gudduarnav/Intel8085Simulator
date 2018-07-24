jmp start
stra: ds " atd*123#"
db 0Dh
db 0ah
start:mvi a,CBh
out 41h

lxi h, stra
mov a, m
out 40h
inx h
mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h

mov a, m
out 40h
inx h


hlt
