public class Instance{
    private string _Name;
    private Form _Form;

    public Instance(string name, Form form){
        _Name = name;
        _Form = form;
    }

    public void Close(){
        _Form.Close();
    }

    public Form GetForm(){
        return _Form;
    }

}