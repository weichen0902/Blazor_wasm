//let chartInstance;

//window.createStreamingChart = async (canvasId, historicalData) => {
//    const ctx = document.getElementById(canvasId).getContext('2d');

//    chartInstance = new Chart(ctx, {
//        type: 'line',
//        data: {
//            datasets: [
//            {
//                label: 'Device#2',
//                borderColor: 'rgb(255, 99, 132)',
//                backgroundColor: 'rgba(255, 99, 132, 0.2)',
//                borderWidth: 3,
//                pointRadius: 1,
//                data: historicalData.map(point => ({
//                    x: new Date(point.dateTime),
//                    y: point.d1pH // 假設你的歷史數據有 yValue1
//                }))
//                },
//                {
//                    label: 'Device#1',
//                    borderColor: 'rgb(75, 192, 192)',
//                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
//                    borderWidth: 3,
//                    pointRadius: 1,
//                    data: historicalData.map(point => ({
//                        x: new Date(point.dateTime),
//                        y: point.d2pH // 假設你的歷史數據有 yValue2
//                    }))
//                },]
//        },
//        options: {
//            plugins: {
//                legend: {
//                    display: true,  // 是否顯示 Legend
//                    position: 'bottom',  // 這裡可改為 'top', 'left', 'right', 'chartArea'

//                }
//            },
//            scales: {
//                x: {
//                    type: 'realtime',
//                    realtime: {
//                        delay: 2000,
//                        duration:300000,
//                        onRefresh: async function (chart) {

//                            let yValue1 = await DotNet.invokeMethodAsync('MauiBlazorAPP', 'GetY1ValueAsync');
//                            let yValue2 = await DotNet.invokeMethodAsync('MauiBlazorAPP', 'GetY2ValueAsync');
//                            /*console.log("Received value:", yValue1);*/
//                            chart.data.datasets[0].data.push({
//                                x: Date.now(),
//                                y: yValue1
//                            });

//                            chart.data.datasets[1].data.push({
//                                x: Date.now(),
//                                y: yValue2
//                            });
//                        }
//                    }
//                },
//                y: {
//                    beginAtZero: true
//                }
//            }
//        }
//    });
//};

//window.toggleStreaming = (pause) => {
//    if (chartInstance) {
//        chartInstance.options.scales.x.realtime.pause = pause;
//        chartInstance.update();
//    }
//};

let chartInstance;

window.createStreamingChart = async (canvasId, historicalData) => {
    const ctx = document.getElementById(canvasId).getContext('2d');

    chartInstance = new Chart(ctx, {
        type: 'line',
        data: {
            datasets: [
                {
                    label: 'Device#1',
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderWidth: 3,
                    pointRadius: 1,
                    data: historicalData.map(point => ({
                        x: new Date(point.dateTime),
                        y: point.d1pH
                    }))
                },
                {
                    label: 'Device#2',
                    borderColor: 'rgb(75, 192, 192)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderWidth: 3,
                    pointRadius: 1,
                    data: historicalData.map(point => ({
                        x: new Date(point.dateTime),
                        y: point.d2pH
                    }))
                }
            ]
        },
        options: {
            plugins: {
                legend: {
                    display: true,
                    position: 'bottom',
                    onClick: (e, legendItem) => {
                        const dataset = chartInstance.data.datasets[legendItem.datasetIndex];
                        dataset.hidden = !dataset.hidden; // 切換顯示/隱藏
                        updateYAxisScale(); // 重新計算 Y 軸範圍
                    }
                }
            },
            scales: {
                x: {
                    type: 'realtime',
                    realtime: {
                        delay: 2000,
                        duration: 300000,
                        refresh: 10000,
                        onRefresh: async function (chart) {
                            let yValue1 = await DotNet.invokeMethodAsync('Blazor_wasm', 'GetY1ValueAsync');
                            let yValue2 = await DotNet.invokeMethodAsync('Blazor_wasm', 'GetY2ValueAsync');

                            chart.data.datasets[0].data.push({ x: Date.now(), y: yValue1 });
                            chart.data.datasets[1].data.push({ x: Date.now(), y: yValue2 });

                            updateYAxisScale(); // 每次新增數據時，更新 Y 軸
                        }
                    }
                },
                y: {
                    beginAtZero: false, // 允許動態縮放
                }
            }
        }
    });
};

// **更新 Y 軸範圍**
function updateYAxisScale() {
    if (!chartInstance) return;

    let allData = [];

    // 只考慮可見的數據集
    chartInstance.data.datasets.forEach(dataset => {
        if (!dataset.hidden) {
            allData = allData.concat(dataset.data.map(d => d.y));
        }
    });

    if (allData.length > 0) {
        let minY = Math.min(...allData);
        let maxY = Math.max(...allData);
        let padding = (maxY - minY) * 0.5; // 10% 內邊距

        // 確保最小間距，避免貼齊
        if (padding === 0) padding = 0.5;

        // 更新 Y 軸範圍
        chartInstance.options.scales.y.min = minY - padding;
        chartInstance.options.scales.y.max = maxY + padding;

        chartInstance.update(); // 強制圖表重新渲染
    }
}