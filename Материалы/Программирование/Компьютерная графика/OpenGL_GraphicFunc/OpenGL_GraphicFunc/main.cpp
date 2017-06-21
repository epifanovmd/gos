#include <glut.h> //подключаем заголовочный файл glut.h
#include <math.h>

//Переменные, которые задают границы рисования графика и его шаг
GLfloat start = -4.0f, end = 4.0f, step = 0.02f;
//Надпись функции
const char* str_func = "y(x) = cos(x^2)";

//Функция, которую нужно построить
GLfloat Func(double x)
{
	return (GLfloat)cos(x*x);
	
}

//Функция, строящая график по точкам
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

//Функция, строящая график при помощи линий
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

//Функция выводит текст, по заданным координатам
void Print(GLfloat x, GLfloat y, GLfloat z, const char* string)
{
	glRasterPos3f(x, y, z);
	while (*string)
		glutBitmapCharacter(GLUT_BITMAP_TIMES_ROMAN_24, *string++);
}



void renderScene(void) {


	// очистка буфера цвета и глубины
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	
	
	// обнуление трансформации
	glLoadIdentity();
	// установка позиции камеры 
	gluLookAt(0.0f, 0.0f, end*2,
		0.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f); 

	//строим ось X
	glColor3f(1.0f, 1.0f, 1.0f);
	Print(end, -0.25f, 0, "x");
	glBegin(GL_LINES);
	glColor3f(0.0f, 0.0f, 1.0f);
	glVertex3f(100.0f, 0, 0);
	glVertex3f(-100.0f, 0, 0);
	glEnd();

	//строим ось Y
	glColor3f(1.0f, 1.0f, 1.0f);
	Print(0.25f, end / 1.3f, 0, "y");
	glBegin(GL_LINES);
	glColor3f(1.0f, 0.0f, 0.0f);
	glVertex3f(0, 100.0f, 0);
	glVertex3f(0, -100.0f, 0);
	glEnd();

	glColor3f(1.0f, 1.0f, 1.0f);
	//Выводим надпись функции
	Print(end / 2, end / 1.5, 1, str_func);

	//строим график по точкам
	DrawGraphicWithPoints();

	//строим график при помощи линий
	//DrawGraphicWithLine();
	
	


	glutSwapBuffers();
}
//функция вызывается, когда изменяется размер окна, чтоб сохранить пропорции объектов
void changeSize(int w, int h) {

	// предупредим деление на ноль
	// если окно сильно перетянуто будет
	if (h == 0)
		h = 1;
	float ratio = 1.0* w / h;

	// используем матрицу проекции
	glMatrixMode(GL_PROJECTION);

	// Сбрасываем матрицу к единичной
	glLoadIdentity();

	// определяем окно просмотра
	glViewport(0, 0, w, h);

	// установить корректную перспективу.
	gluPerspective(45, ratio, 1, 1000);

	// вернуться к модели
	glMatrixMode(GL_MODELVIEW);
}



int main(int argc, char **argv) {

	// Инициализация GLUT
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGBA);
	glutInitWindowPosition(100, 50);
	glutInitWindowSize(800, 600);
	glutCreateWindow("Построение графика функции");
	// регистрация обратных вызовов
	glutDisplayFunc(renderScene);

	// регистрация изменения размеров окна
	glutReshapeFunc(changeSize);

	// регистрация функции, которая вызывается, когда приложение находится в режиме ожидания
	glutIdleFunc(renderScene);

	// Основной цикл GLUT
	glutMainLoop();

	return 1;
}