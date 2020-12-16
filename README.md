# ntech.saga
POC Masstransit Saga State Machine

## Bài viết: http://blog.ntechdevelopers.com/cach-xay-dung-mot-workflow-thong-qua-masstransit-saga-state-machine/

## Cách chạy:

- Start all service: Ntech.Saga.Service.Api (Đại diện cho service gateway call từ UI), Ntech.Saga.Service.Management (Đại diện cho service chứa saga state machine), Ntech.Saga.Service.Handlling (Đại diện cho service handle nhận event xử lý từ workflow)

- Trigger event luồng booking happycase từ endpoint /booking trong Ntech.Saga.Service.Api

- Trigger event luồng booking cancelled từ endpoint /booking/cancel trong Ntech.Saga.Service.Api

- Trigger event luồng exception case từ endpoint /booking/fail trong Ntech.Saga.Service.Api