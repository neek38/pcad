# pcad
Creation of our personal pcad

# Правила названия ветки: фамилия_название_модуля (на английском через '_' все маленькие буквы)

## Чтобы создать свою ветку разрабоки необходимо
1. Зарегестрироваться и скачать git command line на свой ПК
2. Создать директорию под проект на своём ПК
3. Открыть созданную директорию в git command line
4. Ввести по очереди комманды:
   1. git clone https://github.com/neek38/pcad.git
   2. cd pcad/
   3. git checkout -b название_ветки_без_кавычек_на_анлийском
   4. git add *
   5. git push origin название_ветки_без_кавычек_на_анлийском

## Для фиксации первых изменений в вашем коде необходимо ввести следующую последовательность комманд:
   1. git add *
   2. git commit -a -m "Что вы изменили (в кавычках как в примере)"
   3. git push --set-upstream origin название_ветки_без_кавычек_на_анлийском

## Для фиксации последуюих изменений в вашем коде необходимо ввести следующую последовательность комманд:
   1. git add *
   2. git commit -a -m "Что вы изменили (в кавычках как в примере)"
   3. git push

## Для извлечения изменений (обновления кода после вмешательства другого человека) ввести следующую комманду
   1. git pull
   
## Формат входных и выходных файлов - _JSON_
