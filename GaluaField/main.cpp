#include <vector>
#include <iostream>

class field_elem
{
private:
    unsigned short	field;
    bool get_byte(int n) const;
    field_elem();
public:
    int	number()
    {
        return (field);
    }
    friend field_elem mod(const field_elem &first, const field_elem &second);
    friend field_elem sum(const field_elem &first, const field_elem &second);
    friend field_elem simple_mult(const field_elem &first, const field_elem &second);
    friend field_elem mult(field_elem first, field_elem second);
    friend field_elem reverse(field_elem elem);
    friend field_elem pow(field_elem elem, unsigned int _pow);
    friend std::vector<field_elem> get_all_irreducible_poly();
    friend std::ostream& operator<<(std::ostream &out, const field_elem &elem);

    field_elem(const field_elem &elem);
    field_elem(unsigned short field);
    ~field_elem();
};

bool field_elem::get_byte(int n) const
{
    return (((field >> (n)) & 1) > 0);
}

field_elem::field_elem(const field_elem &elem)
{
    this->field = elem.field;
}

field_elem::field_elem(unsigned short n)
{
    field = n;
}

field_elem simple_mult(const field_elem &first, const field_elem &second)
{
    field_elem	ans(0);

    for (int i = 7; i >= 0; i--)
    {
        if (second.get_byte(i))
        {
            ans = sum(ans, field_elem(first.field << i));
        }
    }

    return (ans);
}

field_elem mod(const field_elem &first, const field_elem &second)
{
    field_elem ans(first.field);
    field_elem der(0);
    int max_pow_of_first = 0;
    int j;
    j = 16;
    while (j >= 0)
    {
        {
            if (second.get_byte(j))
                break ;
            j--;
        }
    }

    while (second.field)
    {
        for (int i = 0; i < 16; i++)
            if (ans.get_byte(i))
                max_pow_of_first = i;
        if (j > 0)
            ans = sum(ans, second.field << (max_pow_of_first - j));
        if (j >= max_pow_of_first)
            break ;
    }
    return (ans);
}

std::ostream& operator<<(std::ostream &out, const field_elem &elem)
{
    int j = 0;

    if (elem.field == 0)
        out << "0";

    for (int i = 14; i >= 0; i--)
    {
        if (elem.get_byte(i))
        {
            if (j != 0)
            {
                out << " + ";
            }
            if (i == 1)
            {
                std::cout << "x";
            }
            if (i == 0)
            {
                std::cout << "1";
            }
            if (i > 1)
            {
                std::cout << "x^" << i;
            }
            j++;
        }
    }
    return (out);
}

field_elem mult(field_elem first, field_elem second)
{
    return (mod(simple_mult(first, second), field_elem(283)));
}

field_elem pow(field_elem elem, unsigned int _pow)
{
    if (_pow == 0)
        return (field_elem(1));
    else if (_pow % 2)
        return (mult(elem, pow(elem, _pow - 1)));
    return (pow(mult(elem, elem), _pow / 2));
}

field_elem sum(const field_elem &first, const field_elem &second)
{
    field_elem ans((first.field ^ second.field));
    return (ans);
}

field_elem reverse(field_elem elem)
{
    return (pow(elem, 254));
}

std::vector<field_elem>	get_all_irreducible_poly()
{
    std::vector <field_elem> ans;

    int j;
    for (int i = 257; i < 512; i += 2)
    {
        field_elem	a(i);
        for (j = 2; j < 32; j++)
        {
            field_elem	b(j);
            if (mod(a, b).field == 0)
            {
                j = 2;
                break;
            }
        }
        if (j == 32)
            ans.push_back(a);
    }
    return (ans);
}

int	main()
{
    field_elem	f1(125);
    field_elem	f2(133);

    std::cout << f1 << " + " << f2 << " =" << std::endl;
    std::cout <<  sum(f1, f2) << std::endl;
    std::cout << pow(f1, 2) << " = " << pow(f1, 2).number() << std::endl;
    std::cout << mult(f1, reverse(f1)) << std::endl;

    std::cout << "irred num - " << get_all_irreducible_poly().size() << std::endl;

}