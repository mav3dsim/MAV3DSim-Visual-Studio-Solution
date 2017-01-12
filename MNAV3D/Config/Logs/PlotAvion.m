% Parameters
%FT = 250;
FT=TF;
%close all
figure1 = figure;% Create axes
axes1 = axes('Parent',figure1);
view(axes1,[40.5 12]);
grid(axes1,'on');
hold(axes1,'all');
plot3(xEastObj,yNorthObj,zUpObj,'r');
hold on
plot3(xEast,yNorth,zUp,'Parent',axes1,'DisplayName','Aircraft');
%comet3(x.Data,y.Data,z.Data);



% Create title
title('3D Path following');

% Create xlabel
xlabel('Meters (m)','HorizontalAlignment','right');

% Create ylabel
ylabel('Meters (m)','HorizontalAlignment','left');

% Create zlabel
zlabel('Meters (m)');

% Create legend
legend1 = legend(axes1,'show');
set(legend1,...
    'Position',[0.709867825494983 0.660137987100387 0.187697160883281 0.0915032679738562]);


% Create figure
figure2 = figure;

% Create subplot
subplot1 = subplot(2,1,1,'Parent',figure2,'YGrid','on','XGrid','on');
%% Uncomment the following line to preserve the X-limits of the axes
 xlim(subplot1,[0 FT]);
%% Uncomment the following line to preserve the Y-limits of the axes
 ylim(subplot1,[-10 5]);
box(subplot1,'on');
hold(subplot1,'all');

% Create multiple lines using matrix input to plot
plot1_ = plot(ex,'Parent',subplot1);
plot2_ = plot(ey,'Parent',subplot1);
plot3_ = plot(ez,'Parent',subplot1);
set(plot1_,'DisplayName','e_x');
set(plot2_,'Color',[1 0 0],'DisplayName','e_y');
set(plot3_,'Color',[0 1 0],'DisplayName','e_z');

% Create xlabel
xlabel(' Time (s)');

% Create ylabel
ylabel(' Meters (m)');

% Create title
title(' Position errors: e_x, e_y, e_z');

% Create subplot
subplot2 = subplot(2,1,2,'Parent',figure2,'YGrid','on','XGrid','on');
%% Uncomment the following line to preserve the X-limits of the axes
 xlim(subplot2,[0 FT]);
%% Uncomment the following line to preserve the Y-limits of the axes
 ylim(subplot2,[-1 1]);
box(subplot2,'on');
hold(subplot2,'all');

% Create plot
plot(e_psi,'Parent',subplot2,'DisplayName','e_\psi');

% Create xlabel
xlabel('Time (s)');

% Create ylabel
ylabel('radians(°)');

% Create title
title('Angular error: e_\psi');

% Create legend
legend2 = legend(subplot1,'show');
set(legend2,...
    'Position',[0.790977829404985 0.808695902269966 0.11198738170347 0.171387073347858]);

% Create legend
legend(subplot2,'show');



% Create figure
figure3 = figure;

% Create subplot
subplot3 = subplot(2,1,1,'Parent',figure3,'YGrid','on','XGrid','on');
%% Uncomment the following line to preserve the X-limits of the axes
 xlim(subplot3,[0 FT]);
%% Uncomment the following line to preserve the Y-limits of the axes
 ylim(subplot3,[-.25 .25]);
box(subplot3,'on');
hold(subplot3,'all');

% Create multiple lines using matrix input to plot
plot_phi = plot(phi,'Parent',subplot3);
plot_theta = plot(theta,'Parent',subplot3);

set(plot_phi,'DisplayName','\phi');
set(plot_theta,'Color',[1 0 0],'DisplayName','\theta');


% Create xlabel
xlabel(' Time (s)');

% Create ylabel
ylabel(' radians (°)');

% Create title
title(' Controllers: \phi, \theta');

% Create subplot
subplot4 = subplot(2,1,2,'Parent',figure3,'YGrid','on','XGrid','on');
%% Uncomment the following line to preserve the X-limits of the axes
 xlim(subplot4,[0 FT]);
%% Uncomment the following line to preserve the Y-limits of the axes
 ylim(subplot4,[5 15]);
box(subplot4,'on');
hold(subplot4,'all');

% Create plot
plot(s_dot,'Parent',subplot4,'DisplayName','s');

% Create xlabel
xlabel('Time (s)');

% Create ylabel
ylabel('Particle speed(m/s)');

% Create title
title('Control: s');

% Create legend
legend3 = legend(subplot3,'show');
set(legend3,...
    'Position',[0.790977829404985 0.808695902269966 0.11198738170347 0.171387073347858]);

% Create legend
legend(subplot4,'show');