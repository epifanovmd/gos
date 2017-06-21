#include <glut.h> //���������� ������������ ���� glut.h
#include <math.h>

//����������, ������� ������ ������� ��������� ������� � ��� ���
GLfloat start = -4.0f, end = 4.0f, step = 0.02f;
//������� �������
const char* str_func = "y(x) = cos(x^2)";

//�������, ������� ����� ���������
GLfloat Func(double x)
{
	return (GLfloat)cos(x*x);
	
}

//�������, �������� ������ �� ������
void DrawGraphicWithPoints()
{
	glColor3f(0, 1, 0);
	glBegin(GL_POINTS);
	for (GLfloat x = start; x <= end; x += step)
	{		
		glVertex3f(x, Func(x), 0);	
	}
	glEnd();
}

//�������, �������� ������ ��� ������ �����
void DrawGraphicWithLine()
{
	glColor3f(0, 1, 0);
	glBegin(GL_LINE_STRIP);
	for (GLfloat x = start; x <= end; x += step)
	{
		glVertex3f(x, Func(x), 0);
	}
	glEnd();
}

//������� ������� �����, �� �������� �����������
void Print(GLfloat x, GLfloat y, GLfloat z, const char* string)
{
	glRasterPos3f(x, y, z);
	while (*string)
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, *string++);
}



void renderScene(void) {


	// ������� ������ ����� � �������
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	
	
	// ��������� �������������
	glLoadIdentity();
	// ��������� ������� ������ 
	gluLookAt(0.0f, 0.0f, end*2,
		0.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f); 

	//������ ��� X
	glColor3f(1.0f, 1.0f, 1.0f);
	Print(end, -0.25f, 0, "x");
	glBegin(GL_LINES);
	glColor3f(0.0f, 0.0f, 1.0f);
	glVertex3f(100.0f, 0, 0);
	glVertex3f(-100.0f, 0, 0);
	glEnd();

	//������ ��� Y
	glColor3f(1.0f, 1.0f, 1.0f);
	Print(0.25f, end / 1.3f, 0, "y");
	glBegin(GL_LINES);
	glColor3f(1.0f, 0.0f, 0.0f);
	glVertex3f(0, 100.0f, 0);
	glVertex3f(0, -100.0f, 0);
	glEnd();

	glColor3f(1.0f, 1.0f, 1.0f);
	//������� ������� �������
	Print(end / 2, end / 1.5, 1, str_func);

	//������ ������ �� ������
	DrawGraphicWithPoints();

	//������ ������ ��� ������ �����
	//DrawGraphicWithLine();
	
	


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



int main(int argc, char **argv) {

	// ������������� GLUT
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 50);
	glutInitWindowSize(800, 600);
	glutCreateWindow("���������� ������� �������");
	// ����������� �������� �������
	glutDisplayFunc(renderScene);

	// ����������� ��������� �������� ����
	glutReshapeFunc(changeSize);

	// ����������� �������, ������� ����������, ����� ���������� ��������� � ������ ��������
	glutIdleFunc(renderScene);

	// �������� ���� GLUT
	glutMainLoop();

	return 1;
}