#include <fcntl.h>
#include <unistd.h>
#include <stdio.h>

int main(void) {
  int data[3] = {62, 63, 64};
  int inbuf;
  int fd;
  int count;
  
  fd = open("/tmp/l1/data.dat", O_WRONLY | O_CREAT, 0666);
  if (fd == -1) {
    perror("ERROR");
    return 1;
  }

  count = write(fd, data, 3 * sizeof(int));
  if (count == -1) {
    perror("ERROR");
    return 1;
  }
  // if (count != 3 * sizeof(int)) {...}

  close(fd);
  // FIXME: error checking

  fd = open("/tmp/l1/data.dat", O_RDONLY);
  if (fd == -1) {
    perror("ERROR");
    return 1;
  }

  lseek(fd, sizeof(int), SEEK_SET);
  
  while(1) {
    count = read(fd, &inbuf, sizeof(int));
    if (count == -1) {
      perror("ERROR");
      return 1;
    }
    if (count == 0)
      break;
    printf("%d\n", inbuf);
  }

  close(fd);
  
  return 0;
}
