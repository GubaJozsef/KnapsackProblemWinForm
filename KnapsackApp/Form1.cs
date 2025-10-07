namespace KnapsackApp;

using System;
using System.Windows.Forms;
using System.Collections.Generic;

public partial class Form1 : Form
{
    private TextBox txtName, txtWeight, txtValue, txtCapacity;
    private Button btnAdd, btnSolve;
    private ListBox lstItems;
    private Label lblCapacity, lblResult;

    private List<Item> items = new List<Item>();
    public class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return Name + " (Súly: " + Weight + "kg, Érték: " + Value + "$)";
        }
    }


    public Form1()
    {
        InitializeComponent();

        // Név beviteli mező
        txtName = new TextBox();
        txtName.Location = new System.Drawing.Point(10, 10);
        txtName.Width = 100;
        txtName.PlaceholderText = "Név";
        this.Controls.Add(txtName);

        // Súly beviteli mező
        txtWeight = new TextBox();
        txtWeight.Location = new System.Drawing.Point(120, 10);
        txtWeight.Width = 50;
        txtWeight.Height = 30;
        txtWeight.PlaceholderText = "Súly";
        this.Controls.Add(txtWeight);

        // Érték beviteli mező
        txtValue = new TextBox();
        txtValue.Location = new System.Drawing.Point(190, 10);
        txtValue.Width = 50;
        txtName.Height = 30;
        txtValue.PlaceholderText = "Érték";
        this.Controls.Add(txtValue);

        // Hozzáadás gomb
        btnAdd = new Button();
        btnAdd.Location = new System.Drawing.Point(260, 10);
        btnAdd.Width = 90;
        btnAdd.Height = 30;
        btnAdd.Text = "Hozzáadás";
        btnAdd.Click += BtnAdd_Click;
        this.Controls.Add(btnAdd);

        // Lista, ahol megjelennek a tárgyak
        lstItems = new ListBox();
        lstItems.Location = new System.Drawing.Point(10, 50);
        lstItems.Width = 280;
        lstItems.Height = 200;
        this.Controls.Add(lstItems);

        //Kapacitás felirat
        lblCapacity = new Label();
        lblCapacity.Location = new System.Drawing.Point(10, 260);
        lblCapacity.Width = 200;
        lblCapacity.Height = 30;
        lblCapacity.Text = "Hátizsák kapacitása (kg):";
        this.Controls.Add(lblCapacity);

        //Kapacitás beviteli mező
        txtCapacity = new TextBox();
        txtCapacity.Location = new System.Drawing.Point(lblCapacity.Location.X + lblCapacity.Width + 10, 260);
        txtCapacity.Width = 50;
        txtCapacity.Text = "10";
        this.Controls.Add(txtCapacity);

        // Megoldás keresése gomb
        btnSolve = new Button();
        btnSolve.Location = new System.Drawing.Point(10, 310);
        btnSolve.Width = 180;
        btnSolve.Height = 30;
        btnSolve.Text = "Megoldás keresése";
        btnSolve.Click += BtnSolve_Click;
        this.Controls.Add(btnSolve);

        // Eredmény felirat
        lblResult = new Label();
        lblResult.Location = new System.Drawing.Point(10, 350);
        lblResult.Width = 400;
        lblResult.Height = 90;
        lblResult.Text = "";
        this.Controls.Add(lblResult);



        var exampleItems = new List<Item>
        {
            new Item { Name = "Laptop", Weight = 5, Value = 1000 },
            new Item { Name = "Könyv", Weight = 4, Value = 30 },
            new Item { Name = "Labda", Weight = 2, Value = 10 }
        };
        foreach (var item in exampleItems)
        {
            items.Add(item);
            lstItems.Items.Add(item);
        }
    }


    private void BtnAdd_Click(object sender, EventArgs e)
    {
        // Ellenőrizzük, hogy minden mező ki van-e töltve, és a számok helyesek-e
        if (string.IsNullOrWhiteSpace(txtName.Text) || !int.TryParse(txtWeight.Text, out int weight) || !int.TryParse(txtValue.Text, out int value))
        {
            MessageBox.Show("Adj meg minden adatot helyesen!");
            return;
        }
        // Új tárgy létrehozása
        Item item = new Item();
        item.Name = txtName.Text;
        item.Weight = weight;
        item.Value = value;

        // Mezők ürítése
        txtName.Text = "";
        txtWeight.Text = "";
        txtValue.Text = "";

        items.Add(item);
        lstItems.Items.Add(item);
    }

    private void BtnSolve_Click(object sender, EventArgs e)
    {
        // Kapacitás beolvasása
        if (!int.TryParse(txtCapacity.Text, out int capacity))
        {
            MessageBox.Show("Adj meg egy érvényes számot a kapacitásnak!");
            return;
        }

        int totalWeight = 0;
        int totalValue = 0;
        List<Item> selectedItems = new List<Item>();
        
        // Egyszerű algoritmus: sorban hozzáadja a tárgyakat, amíg belefér
        foreach (var item in items)
        {
            if (totalWeight + item.Weight <= capacity)
            {
                selectedItems.Add(item);
                totalWeight += item.Weight;
                totalValue += item.Value;
            }
        }
        // Eredmény kiírása
        if (selectedItems.Count == 0)
        {
            lblResult.Text = "Nem lehet tárgyat betenni a hátizsákba!";
        }
        else
        {
            lblResult.Text = "Kiválasztott tárgyak: " +
                string.Join(", ", selectedItems.ConvertAll(i => i.Name)) +
                $"  \nÖsszsúly: {totalWeight}kg  \nÖsszérték: {totalValue}$";
        }

    }
}
