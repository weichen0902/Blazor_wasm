window.historyChartInterop = {
    saveDateTime: function (startDateTime, endDateTime) {
        localStorage.setItem('historyChart_startDateTime', startDateTime);
        localStorage.setItem('historyChart_endDateTime', endDateTime);
    },
    getStartDateTime: function () {
        return localStorage.getItem('historyChart_startDateTime');
    },
    getEndDateTime: function () {
        return localStorage.getItem('historyChart_endDateTime');
    }
};
