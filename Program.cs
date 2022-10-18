using System;

Input a = new Input();
Input b = new Input();
Input c = new Input();
AndGate and = new AndGate();
OrGate or = new OrGate();
NotGate not = new NotGate();

b.Input = true;
c.Input = true;

a.Connect(and);
b.Connect(and);
c.Connect(or);
and.Connect(or);
or.Connect(not);

Console.WriteLine(not.Output);

public abstract class Portas
{
    public bool In1 { get; set; }
    public bool In2 { get; set; }
    public bool Input { get; set; }
    public bool Output { get; set; }
    public bool In1C { get; set; }

    public abstract void Connect(Portas target);
    public abstract void ReceiveData(bool entrada);
}


public class OrGate : Portas
{
    public OrGate()
    {
        this.In1 = false;
        this.In2 = false;
        this.In1C = false;
        this.Output = this.In1 || this.In2;
    }

    public override void Connect(Portas target)
    {
        bool entrada = this.Output;
        target.ReceiveData(entrada);
    }

    public override void ReceiveData(bool entrada)
    {
        if (this.In1C == false)
        {
            this.In1 = entrada;
            this.In1C = true;
            this.Output = this.In1 || this.In2;
        }
        else
        {
            this.In2 = entrada;
            this.Output = this.In1 || this.In2;
            this.In1C = false;
        }
    }
}

public class AndGate : Portas
{
    public AndGate()
    {
        this.In1 = false;
        this.In2 = false;
        this.In1C = false;
        this.Output = this.In1 && this.In2;
    }

    public override void Connect(Portas target)
    {
        bool entrada = this.Output;
        target.ReceiveData(entrada);
    }

    public override void ReceiveData(bool entrada)
    {
        if (this.In1C == false)
        {
            this.In1 = entrada;
            this.In1C = true;
            this.Output = this.In1 && this.In2;
        }
        else
        {
            this.In2 = entrada;
            this.Output = this.In1 && this.In2;
            this.In1C = false;
        }
    }
}

public class NotGate : Portas
{
    public NotGate()
    {
        this.In1 = false;
        this.Output = !this.In1;
    }

    public override void Connect(Portas target)
    {
        bool entrada = this.Output;
        target.ReceiveData(entrada);
    }

    public override void ReceiveData(bool entrada)
    {
        this.In1 = entrada;
        this.Output = !this.In1;
    }
}

public class Input : Portas
{
    public Input()
    {
        this.Input = false;
        this.Output = this.Input;
    }

    public override void Connect(Portas target)
    {
        bool entrada = this.Output;
        target.ReceiveData(entrada);
    }

    public override void ReceiveData(bool entrada)
    {
        this.In1 = entrada;
        this.Output = this.Input;
    }
}