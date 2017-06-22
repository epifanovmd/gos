import numpy
from keras.datasets import mnist
from keras.models import Sequential
from keras.layers import Dense
from keras.utils import np_utils
from keras.callbacks import ModelCheckpoint
from keras.models import model_from_json

# Загружаем данные
(X_train, y_train), (X_test, y_test) = mnist.load_data()

# Преобразование размерности изображений
X_train = X_train.reshape(60000, 28 * 28)
X_test = X_test.reshape(10000, 28 * 28)

# Нормализация данных
X_train = X_train.astype('float32')
X_test = X_test.astype('float32')
X_train /= 255
X_test /= 255

# Преобразуем метки в категории
Y_train = np_utils.to_categorical(y_train, 10)
Y_test = np_utils.to_categorical(y_test, 10)


# define model
def my_model():
    # Создаем последовательную модель
    model = Sequential()
    # Добавляем уровни сети
    model.add(Dense(512, input_dim=784, init="normal", activation="relu"))
    model.add(Dense(512, input_dim=784, init="normal", activation="relu"))
    model.add(Dense(10, init="normal", activation="softmax"))
    # Компилируем модель
    model.compile(loss="categorical_crossentropy",
                  optimizer="adam", metrics=["accuracy"])
    print(model.summary())
    return model


# Сохранение весов
weights_file = "weights.hdf5"
checkpoint = ModelCheckpoint(
    weights_file, monitor='acc', mode='max', save_best_only=True, verbose=1)

# Обучаем сеть
model = my_model()
model.fit(X_train, Y_train, batch_size=128,
          nb_epoch=20, validation_split=0.1, verbose=1, callbacks=[checkpoint])

# Сохраняем модель
model_json = model.to_json()
with open("model.json", "w") as json_file:
    json_file.write(model_json)

# Оцениваем качество обучения сети на тестовых данных
scores = model.evaluate(X_test, Y_test, verbose=0)
print("Точность работы на тестовых данных: %.2f%%" % (scores[1] * 100))
