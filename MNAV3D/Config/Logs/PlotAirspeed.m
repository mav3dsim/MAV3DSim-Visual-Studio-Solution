function createfigure(YMatrix1)
%CREATEFIGURE(YMATRIX1)
%  YMATRIX1:  matrix of y data

%  Auto-generated by MATLAB on 19-Sep-2016 12:15:32

% Create figure
figure1 = figure;

% Create axes
axes1 = axes('Parent',figure1,...
    'YTickLabel',{'0','5','10','15','20','25','30'},...
    'YGrid','on',...
    'XTickLabel',{'0','1','2','3','4','5','6','7','8'},...
    'XGrid','on');
%% Uncomment the following line to preserve the X-limits of the axes
 xlim(axes1,[0 6500]);
%% Uncomment the following line to preserve the Y-limits of the axes
 ylim(axes1,[-10 310]);
box(axes1,'on');
hold(axes1,'all');

% Create multiple lines using matrix input to plot
plot1 = plot(YMatrix1,'Parent',axes1);
set(plot1(1),'DisplayName','Aircraft airspeed');
set(plot1(2),'Color',[1 0 0],'DisplayName','Airspeed setpoint');

% Create title
title('Airspeed vs Airspeed setpoint');

% Create xlabel
xlabel('Time (s)');

% Create ylabel
ylabel('Airspeed (m/s)');

% Create legend
legend1 = legend(axes1,'show');
set(legend1,...
    'Position',[0.665262735659848 0.782008830022075 0.191335740072202 0.0695364238410596]);

