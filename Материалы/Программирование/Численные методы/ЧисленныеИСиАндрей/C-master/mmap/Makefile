CC=clang

.PHONY: all
all: mmap write read

mmap: mmap.c

write: CFLAGS+=-lrt
write: write.c

read: CFLAGS+=-lrt
read: read.c
