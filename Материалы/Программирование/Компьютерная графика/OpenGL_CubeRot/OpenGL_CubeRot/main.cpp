#include <glut.h> //���������� ������������ ���� glut.h


// ���������� �������� ���������� � ����� �������� � ����, ���������� �������� ��������
GLfloat angleX = 0.0f, angleY = 0.0f, angleZ = 0.0f, deltaAngle = 0.0f;



void renderScene(void) {

	
	// ������� ������ ����� � �������
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	// ��������� �������������
	glLoadIdentity();
	// ��������� ������� ������ 
	gluLookAt(10.0f, 10.0f, 10.0f,
				0.0f, 0.0f, 0.0f,
				0.0f, 1.0f, 0.0f);


	glColor3f(1.0f, 0.5f, 0.25f);


	glRotatef(angleX, 1, 0, 0);
	glRotatef(angleY, 0, 1, 0);
	glRotatef(angleZ, 0, 0, 1);
	glutSolidCube(5.0f);

	
	
	//���� �� ����������� ������, �� �������� ����� �������
/*glBegin(GL_TRIANGLES);
	glVertex3f(-2.0f, -2.0f, 0.0f);
	glVertex3f(0.0f, 2.0f, 0.0);
	glVertex3f(2.0f, -2.0f, 0.0);
	glEnd();*/

	
	

	glutSwapBuffers();
}

//������� ����������, ����� ���������� ������ ����, ���� ��������� ��������� ��������
void changeSize(int w, int h) {

	// ����������� ������� �� ����
	// ���� ���� ������ ���������� �����
	if (h == 0)
		h = 1;
	float ratio = 1.0* w / h;

	// ���������� ������� ��������
	glMatrixMode(GL_PROJECTION);

	// ���������� ������� � ���������
	glLoadIdentity();

	// ���������� ���� ���������
	glViewport(0, 0, w, h);

	// ���������� ���������� �����������.
	gluPerspective(45, ratio, 1, 1000);

	// ��������� � ������
	glMatrixMode(GL_MODELVIEW);
}

//��������� ������� ������ �������, ��� ����� ����������� ��������
void processSpecialKeys(int key, int x, int y) {
	switch (key) 
	{
		case GLUT_KEY_DOWN:
			angleX += (10.0f + deltaAngle);
			break;
		case GLUT_KEY_UP:
			angleX -= (10.0f + deltaAngle);
			break;
		case GLUT_KEY_LEFT:
			angleY -= (10.0f + deltaAngle);
			break;
		case GLUT_KEY_RIGHT:
			angleY += (10.0f + deltaAngle);
			break;
	}
}

//��������� ������� ������ + � -, ��� ����� ���������� � ���������� �������� ��������
void processNormalKeys(unsigned char key, int x, int y) {
	switch (key)
	{
	case '+': deltaAngle += 5.0f;
		break;
	case '-': deltaAngle -= 5.0f;
		break;
	}
}

int main(int argc, char **argv) {

	// ������������� GLUT
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(700, 500);
	glutCreateWindow("�������� ����");
	// ����������� �������� �������
	glutDisplayFunc(renderScene);

	// ����������� ��������� �������� ����
	glutReshapeFunc(changeSize);

	// ����������� �������, ������� ����������, ����� ���������� ��������� � ������ ��������
	glutIdleFunc(renderScene);

	// ����������� �������, �������������� ����. �������
	glutSpecialFunc(processSpecialKeys);
	glutKeyboardFunc(processNormalKeys);

	// ��������� ���� ���������� OpenGL
	glEnable(GL_ALPHA_TEST); // ���� ������������
	glEnable(GL_DEPTH_TEST); // �������� ��������� ������������
	glEnable(GL_COLOR_MATERIAL); // ���������� ������� ����� ��������� � ����� ��������� ��������
	glEnable(GL_LIGHTING); // ���������� ������������� ���������� �����
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);// ���������� �������� ������
	glEnable(GL_BLEND); // ���������� �������� ������
	glEnable(GL_LIGHT0); // ��������� �������� ��������� �����
	// ������� ��������� � ����������� �������� ��������� �����
	float pos[4] = { 3, 3, 3, 1 };
	float dir[3] = { -1, -1, -1 };
	glLightfv(GL_LIGHT0, GL_POSITION, pos);
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, dir);

	// �������� ���� GLUT
	glutMainLoop();

	return 1;
}