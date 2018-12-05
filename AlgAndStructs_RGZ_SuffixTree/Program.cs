﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgAndStructs_RGZ_SuffixTree
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var testString =
                @"Дон ли, Волга ли течёт, котомку на плечо
Боль в груди — там тайничок, открытый фомкой, не ключом
Сколько миль еще, перелет короткий был не в счет
Долгий пыльный чёс, фургон набит коробками с мерчём

Верим — подфартит, наши постели портативны
Менестрелю два пути — корпоратив или квартирник
Схемы однотипны, все теперь MC
Ведь смену породив, мы здесь достигли смены парадигмы

Теперь рэп — многопартийный, бэтлов наплодив
Я смотрю в зеркало по типу: «Сколько бед наворотил ты!»
Я б весь рэп поработил, но все время в пути
У индустрии нервный тик, валокордин, стенокардийным

Соберите суд, но победителей не судят
Мы первые кроманьонцы, мы выбились в люди
Не пизди, я кладу на вас, челядь, пятикратно
Ведь мы выступаем сильно, будто челюсть питекантропа

Припев:
Весь мой рэп, если коротко, про то, что
Уж который год который город под подошвой
В гору когда прёт, потом под гору когда тошно
Я не то что Гулливер, но все же город под подошвой

Город под подошвой, город под подошвой
Светофоры, госпошлины, сборы и таможни
Я не знаю, вброд или на дно эта дорожка,
Ты живешь под каблуком, у меня город под подошвой

Второй Куплет:
Мимо тополей и спелого хлеба полей,
Где в приведения Есенина, крест, молебен, елей
Из минивена вижу землю, вижу небо над ней,
Мы все преодолеем, если нет, то я не водолей.
Наша Земля топит одиночек, как щенят
Был чужой, но ОХРА,Porchy, Илья больше чем семья!
Бомбу ночью сочинял, что есть мочи начинял
Я так хотел принадлежать чему-то большему, чем я.
Мир пустой, хоть с каждым вторым перезнакомься
Я не биоробот с позитивной лыбой комсомольца
Эй, избавь меня от ваших панацей, домашних парацельс
Ведь для меня ебашить самоцель, подустал
Нам насрать, Тони Старк, как стандарт пара стран,
Автострад, Краснодар, Татарстан.
Москвабад паспорта, нам эстрад нарасхват
Хоть по МКАДу на старт, хоть на Мадагаскар,
Ты знаешь

Припев:
Весь мой рэп, если коротко, про то, что
Уж который год который город под подошвой
В гору когда прёт, потом под гору когда тошно
Я не то что Гулливер, но все же город под подошвой

Город под подошвой, город под подошвой
Светофоры, госпошлины, сборы и таможни
Я не знаю, вброд или на дно эта дорожка,
Ты живешь под каблуком, у меня город под подошвой

Третий Куплет:
Дай силенок тут, не свернуть и не сломаться
Есть маршруты, есть на трассе населенный пункт
И там нас сегодня ждут, нытик не будь женственным
У Руслана в деке саундтреки к путешествию
Снова ебло заспано, снова подъем засветло
Снова броник, снова дорога, мешок за спину
Все наскоро, в поле насрано, дождь, пасмурно
Мост в Азгард, после вас просто везет с транспортом
Я делаю каждый свой куплет автопортретом
Час на чек читаем рэп, как логопед под марафетом
Трафарет на парапетах, лого на стене везде
Мое ученье всем как магомеда с бафометом
Я звезда, дайте теплый плед и капюшон салфетки
Жопу вытирать и все отметка хорошо
Раньше говорили я бы с ним в разведку не пошел
Я с тобой в тур не поехал, ты проверку не прошел
Хоуми, знай!

Припев:
Мой рэп, если коротко, про то, что
Уж который год который город под подошвой
В гору когда прёт, потом под гору когда тошно
Я не то что Гулливер, но все же город под подошвой

Город под подошвой, город под подошвой
Светофоры, госпошлины, сборы и таможни
Я не знаю, вброд или на дно эта дорожка,
Ты живешь под каблуком, у меня город под подошвой


Охра:
Что ты видел за моей улыбкой?
Три года, как в дороге, но мы не улитки
С миру по нитке, крестины - поминки
Палеолитный принцип: миллион в твоей калитке
Я не ценю отмазки подлецов
Но так ли важно чьё под этой маскою лицо?
Покамест колесим в дали для вас,
И нас утаскивает в сон этою тряскою рессор
Охра - что б это ни значило для вас
Автор за вычетом слов, дохлая кляча и балласт
Неоплаченный баланс, как заночевать?
А дальше неудача не про нас
Лишь едва приспичит нам потребность
Собираем город по кирпичикам, как тетрис
Трасса, липкая, как пластырь изоленты
Даже если я устал, похуй, за маской незаметно$";

            var arr = testString.ToCharArray();

            SuffixTree tree = new SuffixTree();

            tree.AddRange(testString);

            for (int i = 1; i < arr.Length; i++)
            {
                var tempArr = new char[i];

                for (int j = 0; j < tempArr.Length; j++)
                {
                    tempArr[j] = arr[arr.Length - i + j];
                }

                var result = tree.IsSuffixExist(new string(tempArr));

                if (!result)
                {
                    throw new Exception();
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
