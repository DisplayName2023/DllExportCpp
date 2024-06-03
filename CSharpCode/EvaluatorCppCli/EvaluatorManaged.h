#pragma once


public ref class EvaluatorManaged
{
public:
    System::Nullable<double> Calculate(System::String^ expression, double x, double y);

};