#include <fcntl.h>
#include <sys/mman.h>
#include <stdint.h>
#include <unistd.h>

int main(void) {
  int fd;
  uint8_t *data;

  fd = open("data", O_RDWR);
  data = (uint8_t*) mmap(NULL, 10, PROT_READ | PROT_WRITE,
			 MAP_SHARED, fd, 0);

  for(int i = 0; i < 10; i++)
    data[i] = i;
  
  munmap(data, 10);
  close(fd);
  
  return 0;
}
